using UnityEngine;

namespace Abstractions.Commands
{
    public interface IAttackCommand
    {
        public IAttackable Target { get; }
    }
}