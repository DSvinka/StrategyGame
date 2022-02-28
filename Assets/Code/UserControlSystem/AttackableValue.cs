using Abstractions;
using UnityEngine;

namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Game/Values/"+nameof(AttackableValue))]
    public class AttackableValue : StatelessSubscribeValueBase<IAttackable>
    {
        
    }
}