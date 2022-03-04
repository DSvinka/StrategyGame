using System.Threading.Tasks;
using Abstractions;

namespace Core.CommandExecutors
{
    public sealed class SetRallyPointCommandExecutor : CommandExecutorBase<ISetRallyPointCommand>
    {
        public override Task ExecuteSpecific(ISetRallyPointCommand command)
        {
            GetComponent<MainBuilding>().RallyPoint = command.RallyPoint;
            return Task.CompletedTask;
        }
    }
}