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
        [SerializeField] private AttackableValue _attackableRMB;
        [SerializeField] private Vector3Value _groundClickRMB;

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
            
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray);
            
            if (Input.GetMouseButtonUp(0))
            {
                if (WeHit<ISelectable>(hits, out var selectable))
                {
                    _selectedObject.SetValue(selectable);
                };
            }
            else if (Input.GetMouseButtonUp(1))
            {
                if (WeHit<IAttackable>(hits, out var attackable))
                {
                    _attackableRMB.SetValue(attackable);
                }
                
                else if (_groundPlane.Raycast(ray, out var enter))
                {
                    _groundClickRMB.SetValue(ray.origin + ray.direction * enter);
                };
            }
        }
        
        private bool WeHit<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = default;
            if (hits.Length == 0)
                return false;

            result = hits
                .Select(hit => hit.collider.GetComponent<T>())
                .FirstOrDefault(c => c != null);
            return result != default;
        }
    }
}