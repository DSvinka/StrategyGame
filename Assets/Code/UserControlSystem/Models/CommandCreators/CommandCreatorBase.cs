using System;
using Abstractions;
using UnityEngine;

namespace UserControlSystem.Models.CommandCreators
{
    public abstract class CommandCreatorBase<T>
    {
        public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
        {
            var specificCommandExecutor = commandExecutor as CommandExecutorBase<T>;
            if (specificCommandExecutor != null)
            {
                SpecificCommand(callback);
            }

            return commandExecutor;
        }

        protected abstract void SpecificCommand(Action<T> creationCallback);

        public virtual void ProcessCancel() { }
    }
}