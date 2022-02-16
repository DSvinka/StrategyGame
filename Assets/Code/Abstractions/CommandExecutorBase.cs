using UnityEngine;

namespace Abstractions
{
    public abstract class CommandExecutorBase<T>: MonoBehaviour, ICommandExecutor
    {
        public void Execute(object command) => ExecuteSpecific((T)command);
        public abstract void ExecuteSpecific(T command);

    }
}