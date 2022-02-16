using System;
using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControlSystem.Models;

namespace UserControlSystem.Presenters
{
    internal sealed class MouseInteractionPresenter: MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private EventSystem _eventSystem;
        
        [SerializeField] private SelectableValue _selectedObject;
        [SerializeField] private Vector3Value _RMBClickPositionObject;
        
        [SerializeField] private Transform _groundTransform;

        private Plane _groundPlane;

        private void Start()
        {
            _groundPlane = new Plane(_groundTransform.up, 0);
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButtonUp(1))
                return;
            
            if (_eventSystem.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButtonUp(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    var selectable = hit.collider.GetComponentInParent<ISelectable>();
                    if (selectable == null)
                        return;

                    _selectedObject.SetValue(selectable);
                };
            }
            else if (Input.GetMouseButtonUp(1))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (_groundPlane.Raycast(ray, out var enter))
                {
                    _RMBClickPositionObject.SetValue(ray.origin + ray.direction * enter);
                }
            }
        }
    }
}