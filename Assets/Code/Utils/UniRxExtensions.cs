using System;
using UniRx;

namespace Utils
{
    public static class UniRxExtensions
    {
        public static IDisposable Subscribe<T1, T2>(this IObservable<ValueTuple<T1, T2>> source, Action<T1, T2> onNext)
        {
            return source.Subscribe(t => onNext(t.Item1, t.Item2));
        }
        
        public static IDisposable Subscribe<T1>(this IObservable<CollectionAddEvent<T1>> source, Action<T1, int> onNext)
        {
            return source.Subscribe(t => onNext(t.Value, t.Index));
        }
    }
}