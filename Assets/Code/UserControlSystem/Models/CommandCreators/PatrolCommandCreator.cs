using System;
using Abstractions.Commands;
using UnityEngine;
using UserControlSystem.Commands;
using Utils;
using Zenject;

namespace UserControlSystem.Models.CommandCreators
{
    public sealed class PatrolCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private AssetsContext _context;
        [Inject] private SelectableValue _selectable;
        
        private Action<IPatrolCommand> _creationCallback;

        [Inject]
        private void Init(Vector3Value groundClicks)
        {
            groundClicks.OnUpdateValue += OnUpdateValue;
        }

        private void OnUpdateValue(Vector3 groundClick)
        {
            _creationCallback?.Invoke(_context.Inject(new PatrolCommand(_selectable.CurrentValue.PivotPoint.position, groundClick)));
            _creationCallback = null;
        }

        protected override void SpecificCommand(Action<IPatrolCommand> creationCallback)
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