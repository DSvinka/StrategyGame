using System;
using Abstractions.Commands;
using UnityEngine;
using UserControlSystem.Commands;
using Utils;
using Zenject;

namespace UserControlSystem.Models.CommandCreators
{
    public sealed class MoveCommandCreator : CommandCreatorBase<IMoveCommand>
    {
        [Inject] private AssetsContext _context;
        private Action<IMoveCommand> _creationCallback;

        [Inject]
        private void Init(Vector3Value groundClick)
        {
            groundClick.OnUpdateValue += OnUpdateValue;
        }

        private void OnUpdateValue(Vector3 groundClick)
        {
            _creationCallback?.Invoke(_context.Inject(new MoveCommand(groundClick)));
            _creationCallback = null;
        }

        protected override void SpecificCommand(Action<IMoveCommand> creationCallback)
        {
            _creationCallback = creationCallback;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            
            _creationCallback = null;
        }
    }
}