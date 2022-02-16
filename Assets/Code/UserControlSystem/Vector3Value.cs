using System;
using Abstractions;
using UnityEngine;

namespace UserControlSystem.Models
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "Game/"+nameof(Vector3Value))]
    public sealed class Vector3Value : SubscribeValueBase<Vector3>
    {
        
    }
}