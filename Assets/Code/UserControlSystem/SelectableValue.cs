using Abstractions;
using UnityEngine;

namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Game/Values/"+nameof(SelectableValue))]
    public sealed class SelectableValue : StatefulSubscribeValueBase<ISelectable>
    {
        
    }
}
