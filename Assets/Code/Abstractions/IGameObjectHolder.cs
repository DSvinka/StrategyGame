using UnityEngine;

namespace Abstractions
{
    public interface IGameObjectHolder
    {
        Transform PivotPoint { get; }
        GameObject GameObject { get; }
    }
}