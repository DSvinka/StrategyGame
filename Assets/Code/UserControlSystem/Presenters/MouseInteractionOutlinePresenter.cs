using System.Collections.Generic;
using Abstractions;
using UIViews.Code.UserControlSystem.Views;
using UnityEngine;
using UserControlSystem.Models;

namespace UserControlSystem.Presenters
{
    public sealed class MouseInteractionOutlinePresenter: MonoBehaviour
    {
        [SerializeField] private Mode _outlineMode;
        [SerializeField] private Color _outlineColor;
        [SerializeField] private float _outlineWidth;
        [SerializeField] private SelectableValue _selectedValue;
        
        private GameObject _lastSelectedGameObject;

        private void Start()
        {
            _selectedValue.OnSelected += OnSelected;
            OnSelected(_selectedValue.CurrentValue);
        }

        private void OnSelected(ISelectable selected)
        {
            if (_lastSelectedGameObject != null)
            {
                if (selected != null)
                {
                    if (_lastSelectedGameObject.GetInstanceID() == selected.GameObject.GetInstanceID())
                        return;
                    
                    RemoveOutline(_lastSelectedGameObject);
                    AddOutline(selected.GameObject);
                    
                    _lastSelectedGameObject = selected.GameObject;
                }
                else
                {
                    RemoveOutline(_lastSelectedGameObject);
                    _lastSelectedGameObject = null;
                }
            }
            else
            {
                if (selected != null)
                {
                    AddOutline(selected.GameObject);
                    _lastSelectedGameObject = selected.GameObject;
                }
            }
        }

        private void AddOutline(GameObject selectedGameObject)
        {
            var outline = selectedGameObject.AddComponent<OutlineView>();
            outline.OutlineMode = _outlineMode;
            outline.OutlineColor = _outlineColor;
            outline.OutlineWidth = _outlineWidth;
        }

        private void RemoveOutline(GameObject selectedGameObject)
        {
            if (selectedGameObject.TryGetComponent(out OutlineView outline))
                Destroy(outline);
        }
    }
}