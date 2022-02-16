using System;
using Abstractions;
using UnityEngine;

namespace UserControlSystem.Models
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Game/"+nameof(SelectableValue))]
    public sealed class SelectableValue : SubscribeValueBase<ISelectable>
    {
        
    }
}
