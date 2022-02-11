namespace Abstractions.Commands
{
    public interface IUnitCommand
    {
        public CommandType CommandType { get; }
    }

    public enum CommandType
    {
        None = 0,
        Attack = 1,
        Move = 2,
        Stop = 3,
        Patrol =43,
    }
}