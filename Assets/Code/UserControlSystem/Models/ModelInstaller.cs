using Abstractions.Commands;
using UnityEngine;
using UserControlSystem.Models.CommandCreators;
using Utils;
using Zenject;

namespace UserControlSystem.Models
{
    public sealed class ModelInstaller: MonoInstaller
    {
        [SerializeField] private AssetsContext _legacyContext;
        [SerializeField] private Vector3Value _mousePositionOnRMBObject;
        [SerializeField] private TransformValue _mouseHitTransformOnRMBObject;

        public override void InstallBindings()
        {
            Container.Bind<AssetsContext>().FromInstance(_legacyContext);
            Container.Bind<Vector3Value>().FromInstance(_mousePositionOnRMBObject);
            Container.Bind<TransformValue>().FromInstance(_mouseHitTransformOnRMBObject);
            
            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCreator>().AsTransient();
            
            Container.Bind<CommandButtonsModel>().AsTransient();
        }
    }
}