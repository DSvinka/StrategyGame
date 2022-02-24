using System.Runtime.CompilerServices;

namespace Abstractions
{
    public interface IAwaiter<TAwaited> : INotifyCompletion
    {
        bool IsCompleted { get; }
        TAwaited GetResult();
    }
}