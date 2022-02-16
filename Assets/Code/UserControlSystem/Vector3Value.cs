using Abstractions;
using UnityEngine;

namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "Game/Values/"+nameof(Vector3Value))]
    public sealed class Vector3Value : SubscribeValueBase<Vector3>
    {
        
    }
}