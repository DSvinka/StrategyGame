using System;
using System.Threading.Tasks;
using Abstractions;
using Abstractions.Commands;
using Commands;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.CommandExecutors
{
    public sealed class ProduceUnitCommandExecutor: CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;
        
        [SerializeField] private Transform _unitsParent;
        [SerializeField] private int _maxUnitsQueue = 5;
        [Inject] private DiContainer _diContainer;

        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

        private void Update()
        {
            if (_queue.Count == 0)
            {
                return;
            }

            var innerTask = (UnitProductionTask) _queue[0];
            innerTask.LeftTime -= Time.deltaTime;
            if (innerTask.LeftTime <= 0)
            {
                RemoveTaskAtIndex(0);
                var instance = _diContainer.InstantiatePrefab(innerTask.UnitPrefab, transform.position, Quaternion.identity, _unitsParent);
                var factionMember = instance.GetComponent<FactionMember>();
                factionMember.SetFaction(GetComponent<FactionMember>().FactionId);
                
                var queue = instance.GetComponent<ICommandsQueue>();
                var mainBuilding = GetComponent<MainBuilding>();
                queue.EnqueueCommand(new MoveCommand(mainBuilding.RallyPoint));
            }
        }

        private void RemoveTaskAtIndex(int index)
        {
            for (var i = 0; i < _queue.Count - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }
            _queue.RemoveAt(_queue.Count - 1);
        }

        public override Task ExecuteSpecific(IProduceUnitCommand command)
        {
            _queue.Add(new UnitProductionTask(command.ProductionTime, command.UnitName, command.Icon, command.UnitPrefab));
            return Task.CompletedTask;
        }

        public void Cancel(int index)
        {
            RemoveTaskAtIndex(index);
        }
    }
}