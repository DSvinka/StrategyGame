using System;
using System.Linq;
using Abstractions;
using Core;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControlSystem.Models;
using Zenject;
using static Utils.UniRxExtensions;

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

        [Inject]
        private void Init()
        {
            _groundPlane = new Plane(_groundTransform.up, 0);

            var framesStream = Observable.EveryUpdate().Where(_ => !_eventSystem.IsPointerOverGameObject());
            var leftClicksStream = framesStream.Where(_ => Input.GetMouseButtonDown(0));
            var rightClicksStream = framesStream.Where(_ => Input.GetMouseButtonDown(1));

            var leftClicksRay = leftClicksStream.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));
            var rightClicksRay = rightClicksStream.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));

            var leftClicksHitsStream = leftClicksRay.Select(ray => Physics.RaycastAll(ray));
            var rightClicksHitsStream = rightClicksRay.Select(ray => (ray, Physics.RaycastAll(ray)));

            leftClicksHitsStream.Subscribe(hits =>
            {
                if (WeHit<ISelectable>(hits, out var selectable))
                {
                    _selectedObject.SetValue(selectable);
                }
            });

            rightClicksHitsStream.Subscribe((ray, hits) =>
            {
                if (WeHit<IAttackable>(hits, out var attackable))
                {
                    _attackableRMB.SetValue(attackable);
                }
                else if (_groundPlane.Raycast(ray, out var enter))
                {
                    _groundClickRMB.SetValue(ray.origin + ray.direction * enter);
                }
            });
        }

        private bool WeHit<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = default;
            if (hits.Length == 0)
            {
                return false;
            }

            result = hits
                .Select(hit => hit.collider.GetComponent<T>())
                .FirstOrDefault(c => c != null);
            return result != default;
        }
    }
}