using UnityEngine;

namespace Abstractions.Commands
{
    public interface IAttackCommand
    {
        public Transform Target { get; }
    }
}