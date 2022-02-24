using System;
using System.Threading;
using Abstractions;
using Abstractions.Commands;
using UnityEngine;
using UserControlSystem.Commands;
using Utils;
using Zenject;

namespace UserControlSystem.Models.CommandCreators
{
    public sealed class AttackCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
    {
        protected override IAttackCommand CreateCommand(IAttackable argument)
        {
            return new AttackCommand(argument);
        }
    }
}