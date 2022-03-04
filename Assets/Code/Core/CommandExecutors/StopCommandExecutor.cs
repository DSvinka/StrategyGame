using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public override Task ExecuteSpecific(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
            return Task.CompletedTask;
        }
    }
}