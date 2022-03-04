using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using Abstractions.Commands;
using UnityEngine;
using Utils;

namespace Core.CommandExecutors
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;
        [SerializeField] private UnitMovementStop _unitMovementStop;
        [SerializeField] private Animator _animator;
        
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Walk");

        public override async Task ExecuteSpecific(IMoveCommand command)
        {
            _unitMovementStop.NavMeshAgent.destination = command.Target;
            _animator.SetTrigger(Walk);
            
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _unitMovementStop.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
            }
            catch
            {
                _unitMovementStop.NavMeshAgent.isStopped = true;
                _unitMovementStop.NavMeshAgent.ResetPath();
            }

            _stopCommandExecutor.CancellationTokenSource = null;
            _animator.SetTrigger(Idle);
        }
    }
}