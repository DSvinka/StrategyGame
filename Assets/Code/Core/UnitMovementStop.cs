using System;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Core
{
    public sealed class UnitMovementStop: MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private int _throttleFrames = 60;
        [SerializeField] private int _continuityThreshold = 10;
        
        public event Action OnStop;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        
        private void Awake()
        {
            _collisionDetector.Collisions
                .Where(_ => _navMeshAgent.hasPath)
                .Where(collision => collision.collider.GetComponentInParent<MainUnit>() != null)
                .Select(_ => Time.frameCount)
                .Distinct()
                .Buffer(_throttleFrames)
                .Where(buffer =>
                {
                    for (var i = 1; i < buffer.Count; i++)
                    {
                        if (buffer[i] - buffer[i - 1] > _continuityThreshold)
                        {
                            return false;
                        }    
                    }
                    return true;
                })
                .Subscribe(_ =>
                {
                    _navMeshAgent.isStopped = true;
                    _navMeshAgent.ResetPath();
                    OnStop?.Invoke();
                })
                .AddTo(this);
        }

        void Update()
        {
            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        OnStop?.Invoke();
                    }
                }
            }
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter()
        {
            return new StopAwaiter(this);
        }
    }
    
    public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
    {
        private readonly UnitMovementStop _unitMovementStop;

        public StopAwaiter(UnitMovementStop unitMovementStop)
        {
            _unitMovementStop = unitMovementStop;
            _unitMovementStop.OnStop += onStop;
        }

        private void onStop()
        {
            _unitMovementStop.OnStop -= onStop;
            OnFinish(new AsyncExtensions.Void());
        }
    }

}