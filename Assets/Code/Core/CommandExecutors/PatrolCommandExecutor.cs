using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using Abstractions.Commands;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Core.CommandExecutors
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Idle = Animator.StringToHash("Idle");

        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;
        
        public override async Task ExecuteSpecific(IPatrolCommand command)
        {
            var pointFrom = command.From;
            var pointTo = command.To;

            while (true)
            {
                _navMeshAgent.destination = pointTo;
                _animator.SetTrigger(Walk);
                _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();

                try
                {
                    await _stop.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
                }
                catch
                {
                    _navMeshAgent.isStopped = true;
                    _navMeshAgent.ResetPath();
                    break;
                }

                (pointFrom, pointTo) = (pointTo, pointFrom);
            }

            _stopCommandExecutor.CancellationTokenSource = null;
            _animator.SetTrigger(Idle);
        }
    }
}