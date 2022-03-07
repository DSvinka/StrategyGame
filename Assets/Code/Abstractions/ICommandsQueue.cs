namespace Abstractions
{
    public interface ICommandsQueue
    {
        object CurrentCommand { get; }
        
        void EnqueueCommand(object command);
        void Clear();
    }
}