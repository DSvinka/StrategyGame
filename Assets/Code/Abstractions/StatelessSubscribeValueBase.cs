using System;
using UniRx;

namespace Abstractions
{
    public abstract class StatelessSubscribeValueBase<T> : SubscribeValueBase<T>, IObservable<T>
    {
        private ReactiveProperty<T> _dataSource = new ReactiveProperty<T>();
        public override void SetValue(T value)
        {
            base.SetValue(value);
            _dataSource.Value = value;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _dataSource.Subscribe(observer);
        }
    }
}