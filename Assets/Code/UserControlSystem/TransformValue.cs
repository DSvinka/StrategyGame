using Abstractions;
using UnityEngine;

namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(TransformValue), menuName = "Game/Values/"+nameof(TransformValue))]
    public class TransformValue : StatelessSubscribeValueBase<Transform>
    {
        
    }
}