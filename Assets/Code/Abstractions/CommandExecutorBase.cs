using UnityEngine;

namespace Abstractions
{
    public abstract class CommandExecutorBase<T>: MonoBehaviour, ICommandExecutor
    {
        public abstract void Execute(T command);
    }
}