using System;
using UnityEngine;
using Utils;

namespace Abstractions
{
    public abstract class SubscribeValueBase<T>: ScriptableObject, IAwaitable<T>
    {
        public class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
        {
            private readonly SubscribeValueBase<TAwaited> _subscribeValueBase;
            public NewValueNotifier(SubscribeValueBase<TAwaited> subscribeValueBase)
            {
                _subscribeValueBase = subscribeValueBase;
                _subscribeValueBase.OnUpdateValue += OnUpdateValue;
            }

            private void OnUpdateValue(TAwaited obj)
            {
                _subscribeValueBase.OnUpdateValue -= OnUpdateValue;
                OnFinish(obj);
            }
        }
        
        public T CurrentValue { get; private set; }
        public Action<T> OnUpdateValue;

        public virtual void SetValue(T value)
        {
            CurrentValue = value;
            OnUpdateValue?.Invoke(value);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new NewValueNotifier<T>(this);
        }
    }
}