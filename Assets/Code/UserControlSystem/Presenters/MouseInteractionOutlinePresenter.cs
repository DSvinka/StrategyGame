using Abstractions;
using UserControlSystem.Models;
using UserControlSystem.Views;
using UnityEngine;

namespace UserControlSystem.Presenters
{
    public sealed class MouseInteractionOutlinePresenter: MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectedValue;
        
        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectedValue.OnUpdateValue += OnSelected;
            OnSelected(_selectedValue.CurrentValue);
        }

        private void OnSelected(ISelectable selected)
        {
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