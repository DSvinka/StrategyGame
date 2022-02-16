using System;
using Abstractions.Commands;
using UnityEngine;
using UserControlSystem.Commands;
using Utils;
using Zenject;

namespace UserControlSystem.Models.CommandCreators
{
    public sealed class AttackCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] private AssetsContext _context;
        private Action<IAttackCommand> _creationCallback;

        [Inject]
        private void Init(TransformValue groundClicks)
        {
            groundClicks.OnUpdateValue += OnUpdateValue;
        }

        private void OnUpdateValue(Transform transformClick)
        {
            _creationCallback?.Invoke(_context.Inject(new AttackCommand(transformClick)));
            _creationCallback = null;
        }

        protected override void SpecificCommand(Action<IAttackCommand> creationCallback)
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