using System.Linq;
using Abstractions;
using UnityEngine;
using UserControlSystem.Models;

namespace UserControlSystem.Presenters
{
    internal sealed class MouseInteractionPresenter: MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0))
                return;

            var hits = new RaycastHit[1];
            var size = Physics.RaycastNonAlloc(_camera.ScreenPointToRay(Input.mousePosition), hits);
            if (size == 0)
                return;

            var selectable = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .FirstOrDefault();
            
            if (selectable == default)
                return;
            
            _selectedObject.SetValue(selectable);
        }
    }
}