using System;
using UniRx;

namespace Abstractions
{
    public abstract class StatefulSubscribeValueBase<T> : SubscribeValueBase<T>, IObservable<T>
    {
        private Subject<T> _dataSource = new Subject<T>();
        public override void SetValue(T value)
        {
            base.SetValue(value);
            _dataSource.OnNext(value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _dataSource.Subscribe(observer);
        }
    }
}