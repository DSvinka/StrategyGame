using System;
using System.Collections.Generic;
using Abstractions;
using UniRx;
using UserControlSystem.Views;
using UserControlSystem.Models;
using UnityEngine;
using Zenject;

namespace UserControlSystem.Presenters
{
    public sealed class CommandButtonsPresenter: MonoBehaviour
    {
        [SerializeField] private CommandButtonsView _view;
        
        [Inject] private CommandButtonsModel _model;
        [Inject] private IObservable<ISelectable> _selectedValue;
        
        private ISelectable _currentSelectable;

        private void Start()
        {
            _view.OnClick += _model.OnCommandButtonClick;
            _model.OnCommandAccepted += _view.BlockInteractions;
            _model.OnCommandComplete += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
                
            _selectedValue.Subscribe(OnSelected);
        }

        private void OnDestroy()
        {
            _view.OnClick -= _model.OnCommandButtonClick;
            _model.OnCommandAccepted -= _view.BlockInteractions;
            _model.OnCommandComplete -= _view.UnblockAllInteractions;
            _model.OnCommandCancel -= _view.UnblockAllInteractions;
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