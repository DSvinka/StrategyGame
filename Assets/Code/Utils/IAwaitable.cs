namespace Abstractions
{
    public interface IAwaitable<TAwaited>
    {
        IAwaiter<TAwaited> GetAwaiter();
    }
}