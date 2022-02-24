using System;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Core
{
    public sealed class UnitMovementStop: MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        public event Action OnStop;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;

        private void Update()
        {
            if (_navMeshAgent.pathPending) 
                return;
            
            if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
                return;
            
            if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                OnStop?.Invoke();
            }
        }
        
        public IAwaiter<AsyncExtensions.Void> GetAwaiter()
        {
            return new StopAwaiter(this);
        }
    }
    
    public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
    {
        private readonly UnitMovementStop _unitMovementStop;

        public StopAwaiter(UnitMovementStop unitMovementStop)
        {
            _unitMovementStop = unitMovementStop;
            _unitMovementStop.OnStop += OnStop;
        }

        private void OnStop()
        {
            _unitMovementStop.OnStop -= OnStop;
            OnFinish(new AsyncExtensions.Void());
        }
    }
}