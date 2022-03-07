using Abstractions;
using Core.Commands;
using Core.Queues;
using UniRx;
using UnityEngine;

namespace Core
{
    public sealed class AutoAttackAgent: MonoBehaviour
    {
        [SerializeField] private UnitCommandsQueue _queue;

        private void Start()
        {
            AutoAttackEvaluator.AutoAttackCommands
                .ObserveOnMainThread()
                .Where(command => command.Attacker == gameObject)
                .Where(command => command.Attacker != null && command.Target != null)
                .Subscribe(command => autoAttack(command.Target))
                .AddTo(this);
        }

        private void autoAttack(GameObject target)
        {
            _queue.Clear();
            _queue.EnqueueCommand(new AutoAttackCommand(target.GetComponent<IAttackable>()));
        }

    }
}