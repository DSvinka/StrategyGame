using UnityEngine;

namespace Abstractions
{
    public interface ISelectable: IHealthHolder, IGameObjectHolder, IIconHolder
    {
        string Name { get; }
    }
}
