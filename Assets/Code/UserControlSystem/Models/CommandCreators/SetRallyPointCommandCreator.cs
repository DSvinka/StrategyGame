﻿using Abstractions;
using Commands;
using UnityEngine;

namespace UserControlSystem.Models.CommandCreators
{
    public sealed class SetRallyPointCommandCreator: CancellableCommandCreatorBase<ISetRallyPointCommand, Vector3>
    {
        protected override ISetRallyPointCommand CreateCommand(Vector3 argument) => new SetRallyPointCommand(argument);
    }
}