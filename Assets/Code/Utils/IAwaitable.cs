namespace Utils
{
    public interface IAwaitable<TAwaited>
    {
        IAwaiter<TAwaited> GetAwaiter();
    }
}