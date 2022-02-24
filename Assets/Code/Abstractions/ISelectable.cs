using UnityEngine;

namespace Abstractions
{
    public interface ISelectable: IHealthHolder, IGameObjectHolder
    {
        Sprite Icon { get; }
        string Name { get; }
    }
}
