using System.Threading;
using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public override void ExecuteSpecific(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
        }
    }
}