using System.Collections.Generic;
using Abstractions;
using UserControlSystem.Views;
using UserControlSystem.Models;
using UnityEngine;
using Zenject;

namespace UserControlSystem.Presenters
{
    public sealed class CommandButtonsPresenter: MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        [Inject] private CommandButtonsModel _model;

        private ISelectable _currentSelectable;

        private void Start()
        {
            _view.OnClick += _model.OnCommandButtonClick;
            _model.OnCommandAccepted += _view.BlockInteractions;
            _model.OnCommandComplete += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
                
            _selectable.OnUpdateValue += OnSelected;
            OnSelected(_selectable.CurrentValue);
        }

        private void OnDestroy()
        {
            _view.OnClick -= _model.OnCommandButtonClick;
            _model.OnCommandAccepted -= _view.BlockInteractions;
            _model.OnCommandComplete -= _view.UnblockAllInteractions;
            _model.OnCommandCancel -= _view.UnblockAllInteractions;
                
            _selectable.OnUpdateValue -= OnSelected;
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
                return;
            
            if (_currentSelectable != null)
            {
                _model.OnSelectionChanged();
            }

            _currentSelectable = selectable;
            _view.Clear();
            
            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange(selectable.GameObject.GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }
    }
}