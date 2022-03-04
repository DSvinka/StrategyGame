using System;
using Abstractions;

namespace UserControlSystem.Models.CommandCreators
{
    public abstract class CommandCreatorBase<T>
    {
        public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
        {
            if (commandExecutor is ICommandExecutor<T> specificCommandExecutor)
            {
                SpecificCommand(callback);
            }

            return commandExecutor;
        }

        protected abstract void SpecificCommand(Action<T> creationCallback);

        public virtual void ProcessCancel() { }
    }
}