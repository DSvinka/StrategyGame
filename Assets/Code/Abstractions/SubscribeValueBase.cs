using System;
using UnityEngine;

namespace Abstractions
{
    public abstract class SubscribeValueBase<T>: ScriptableObject
    {
        public T CurrentValue { get; private set; }
        public Action<T> OnUpdateValue;

        public virtual void SetValue(T value)
        {
            CurrentValue = value;
            OnUpdateValue?.Invoke(value);
        }
    }
}