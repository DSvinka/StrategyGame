using System;
using Abstractions;
using UnityEngine;

namespace UserControlSystem.Models
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Game/"+nameof(SelectableValue))]
    public sealed class SelectableValue : ScriptableObject
    {
        public ISelectable CurrentValue { get; private set; }
        public Action<ISelectable> OnSelected;

        public void SetValue(ISelectable value)
        {
            CurrentValue = value;
            OnSelected?.Invoke(value);
        }
    }
}
