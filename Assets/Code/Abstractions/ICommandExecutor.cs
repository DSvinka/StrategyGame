namespace Abstractions
{
    public interface ICommandExecutor
    {
        public void Execute(object command);
    }
}