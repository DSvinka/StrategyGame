using System;
using System.ComponentModel;
using Abstractions;
using Abstractions.Commands;
using UnityEngine;
using UserControlSystem.Models;
using UserControlSystem.Models.CommandCreators;
using Utils;
using Zenject;

namespace UserControlSystem.Installers
{
    public class ModelsInstaller: MonoInstaller
    {
        [SerializeField] private AssetsContext _legacyContext;
    
        [SerializeField] private Vector3Value _groundClickRMB;
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private AttackableValue _attackableClickRMB;

        [SerializeField] private Sprite _chomperSprite;

        public override void InstallBindings()
        {
            Container.BindInstances(_legacyContext, _selectable, _groundClickRMB, _attackableClickRMB);
        
            Container.Bind<IAwaitable<IAttackable>>().FromInstance(_attackableClickRMB);
            Container.Bind<IAwaitable<Vector3>>().FromInstance(_groundClickRMB);
            Container.Bind<IAwaitable<ISelectable>>().FromInstance(_selectable);
        
            Container.Bind<IObservable<IAttackable>>().FromInstance(_attackableClickRMB);
            Container.Bind<IObservable<Vector3>>().FromInstance(_groundClickRMB);
            Container.Bind<IObservable<ISelectable>>().FromInstance(_selectable);
        
            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<ISetRallyPointCommand>>().To<SetRallyPointCommandCreator>().AsTransient();

            Container.Bind<float>().WithId("Chomper").FromInstance(5f);
            Container.Bind<string>().WithId("Chomper").FromInstance("Chomper");
            Container.Bind<Sprite>().WithId("Chomper").FromInstance(_chomperSprite);
            
            Container.Bind<CommandButtonsModel>().AsTransient();
            Container.Bind<BottomCenterModel>().FromComponentInHierarchy().AsSingle();
        }
    }
}