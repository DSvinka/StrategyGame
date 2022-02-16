using System;
using System.Linq;
using Abstractions;
using Core;
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
        [SerializeField] private Vector3Value _mousePositionOnRMBObject;
        [SerializeField] private TransformValue _mouseHitTransformOnRMBObject;
        
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
                    _mousePositionOnRMBObject.SetValue(ray.origin + ray.direction * enter);
                }
                
                if (Physics.Raycast(ray, out var hit))
                {
                    var mainUnit = hit.collider.GetComponentInParent<MainUnit>();
                    if (mainUnit == null)
                        return;

                    _mouseHitTransformOnRMBObject.SetValue(mainUnit.transform);
                };
            }
        }
    }
}