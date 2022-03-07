using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using Abstractions.Commands;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Core.CommandExecutors
{
    public partial class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
	{
		private static readonly int Idle = Animator.StringToHash("Idle");
		private static readonly int Attack = Animator.StringToHash("Attack");
		private static readonly int Walk = Animator.StringToHash("Walk");
		
		#region Приватные Поля
		[SerializeField] private Animator _animator;
		[SerializeField] private NavMeshAgent _navMeshAgent;
		[SerializeField] private StopCommandExecutor _stopCommandExecutor;

		[Inject] private IHealthHolder _health;
		[Inject(Id = "AttackDistance")] private float _attackingDistance;
		[Inject(Id = "AttackPeriod")] private int _attackingPeriod;

		private Vector3 _position;
		private Vector3 _targetPosition;
		private Quaternion _rotation;

		private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
		private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
		private readonly Subject<IAttackable> _attackTargets = new Subject<IAttackable>();

		private Transform _targetTransform;
		private AttackOperation _currentAttackOperation;
		#endregion

		#region Свойства
		public IHealthHolder Health => _health;
		
		public Vector3 Position => _position;
		public Quaternion Rotation => _rotation;
		
		public Vector3 TargetPosition => _targetPosition;
		public Subject<Vector3> TargetPositions => _targetPositions;
		public Subject<Quaternion> TargetRotations => _targetRotations;
		public Subject<IAttackable> AttackTargets => _attackTargets;
		public Transform TargetTransform => _targetTransform;

		public float AttackingDistance => _attackingDistance;
		public int AttackingPeriod => _attackingPeriod;
		#endregion

		[Inject]
		private void Init()
		{
		    _targetPositions
		        .Select(value => new Vector3((float)Math.Round(value.x, 2), (float)Math.Round(value.y, 2), (float)Math.Round(value.z, 2)))
		        .Distinct()
		        .ObserveOnMainThread()
		        .Subscribe(StartMovingToPosition);

		    _attackTargets
		        .ObserveOnMainThread()
		        .Subscribe(StartAttackingTargets);

		    _targetRotations
		        .ObserveOnMainThread()
		        .Subscribe(SetAttackRotation);
		}

		private void SetAttackRotation(Quaternion targetRotation)
		{
    		transform.rotation = targetRotation;
		}

		private void StartAttackingTargets(IAttackable target)
		{
			_navMeshAgent.isStopped = true;
			_navMeshAgent.ResetPath();
    		_animator.SetTrigger(Attack);
    		target.AddDamage(GetComponent<IDamageDealer>().Damage);
		}

		private void StartMovingToPosition(Vector3 position)
		{
			_navMeshAgent.destination = position;
    		_animator.SetTrigger(Walk);
		}

		public override async Task ExecuteSpecific(IAttackCommand command)
		{
			_targetTransform = (command.Target as Component)?.transform;
			_currentAttackOperation = new AttackOperation(this, command.Target);
			
    		Update();
            
    		_stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
    		try
    		{
        		await _currentAttackOperation.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
    		}
    		catch
    		{
                _currentAttackOperation.Cancel();
    		}
    		_animator.SetTrigger(Idle);
    		_currentAttackOperation = null;
    		_targetTransform = null;
    		_stopCommandExecutor.CancellationTokenSource = null;
		}

		private void Update()
		{
    		if (_currentAttackOperation == null)
    		{
        		return;
    		}

    		lock(this)
    		{
                var transformThis = transform;
                
                _position = transformThis.position;
    			_rotation = transformThis.rotation;
    			if (_targetTransform != null)
    			{
        			_targetPosition = _targetTransform.position;
    			}
    		}
		}
	}
    
    public class AttackOperation : IAwaitable<AsyncExtensions.Void>
	{
    	public class AttackOperationAwaiter : AwaiterBase<AsyncExtensions.Void>
    	{
        	private AttackOperation _attackOperation;

	        public AttackOperationAwaiter(AttackOperation attackOperation)
       		{
            	_attackOperation = attackOperation;
            	attackOperation.OnComplete += onComplete;
        	}

        	private void onComplete()
        	{
            	_attackOperation.OnComplete -= onComplete;
                OnFinish(new AsyncExtensions.Void());
        	}
    	}

	    private event Action OnComplete;

    	private readonly AttackCommandExecutor _attackCommandExecutor;
    	private readonly IAttackable _target;

    	private bool _isCancelled;

    	public AttackOperation(AttackCommandExecutor attackCommandExecutor, IAttackable target)
    	{
        	_target = target;
        	_attackCommandExecutor = attackCommandExecutor;

	        var thread = new Thread(AttackAlgorithm);
       		thread.Start();
    	}

    	public void Cancel()
    	{
        	_isCancelled = true;
        	OnComplete?.Invoke();
    	}

    	private void AttackAlgorithm(object obj)
    	{
        	while (true)
        	{
            	if (_attackCommandExecutor == null || _attackCommandExecutor.Health.Health == 0 || _target.Health == 0 || _isCancelled)
            	{
                	OnComplete?.Invoke();
                	return;
            	}

            	Vector3 targetPosition;
            	Vector3 ourPosition;
            	Quaternion ourRotation;
                
            	lock (_attackCommandExecutor)
            	{
                	targetPosition = _attackCommandExecutor.TargetPosition;
                	ourPosition = _attackCommandExecutor.Position;
                	ourRotation = _attackCommandExecutor.Rotation;
            	}

            	var vector = targetPosition - ourPosition;
            	var distanceToTarget = vector.magnitude;
            	if (distanceToTarget > _attackCommandExecutor.AttackingDistance)
            	{
                	var finalDestination = targetPosition - vector.normalized * (_attackCommandExecutor.AttackingDistance * 0.9f);
                	_attackCommandExecutor.TargetPositions.OnNext(finalDestination);
                	Thread.Sleep(100);
            	}
            	else if (ourRotation != Quaternion.LookRotation(vector))
            	{
                	_attackCommandExecutor.TargetRotations.OnNext(Quaternion.LookRotation(vector));
            	}
            	else
            	{
                	_attackCommandExecutor.AttackTargets.OnNext(_target);
                	Thread.Sleep(_attackCommandExecutor.AttackingPeriod);
            	}
        	}
    	}

    	public IAwaiter<AsyncExtensions.Void> GetAwaiter()
    	{
        	return new AttackOperationAwaiter(this);
    	}
	}
}