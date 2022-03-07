using System;
using Abstractions;
using UniRx;
using UserControlSystem.Models;
using UserControlSystem.Views;
using UnityEngine;
using Zenject;

namespace UserControlSystem.Presenters
{
    public sealed class MouseInteractionOutlinePresenter: MonoBehaviour
    {
        [Inject] private IObservable<ISelectable> _selectedValue;
        
        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectedValue.Subscribe(OnSelected);
        }

        private void OnSelected(ISelectable selected)
        {
            if (this == null)
                return;

            if (_currentSelectable == selected)
                return;
            
            if (_currentSelectable != null)
                DisableOutline(_currentSelectable.GameObject);
            
            _currentSelectable = selected;
            
            if (selected != null)
            {
                EnableOutline(selected.GameObject);
            }
        }

        private void EnableOutline(GameObject selectedGameObject)
        {
            if (selectedGameObject.TryGetComponent(out OutlineView outline))
                outline.enabled = true;
        }

        private void DisableOutline(GameObject selectedGameObject)
        {
            if (selectedGameObject.TryGetComponent(out OutlineView outline))
                outline.enabled = false;
        }
    }
}