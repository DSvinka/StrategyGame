using System.Threading.Tasks;
using Abstractions;
using UnityEngine;

namespace Core
{
    public abstract class CommandExecutorBase<T>: MonoBehaviour, ICommandExecutor<T> where T : class
    {
        public async Task TryExecuteCommand(object command)
        {
            if (command is T specificCommand)
            {
                await ExecuteSpecific(specificCommand);
            }
        }
        
        public abstract Task ExecuteSpecific(T command);
    }
}