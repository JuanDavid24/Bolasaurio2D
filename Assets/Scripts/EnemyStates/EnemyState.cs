using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyStates
{
    public abstract class EnemyState
    {
        protected EnemyStateManager _stateManager;
        protected EnemyController _enemy;
        public EnemyState(EnemyController enemy, EnemyStateManager stateManager)
        {
            _enemy = enemy;
            _stateManager = stateManager;
        }
        public virtual void EnterState() { }
        public abstract void HandleState();
        public virtual void PhysicsUpdate() { }
        public virtual void ExitState() { }
        public virtual void OnAttacked() { }
        public virtual void OnCollisionWithPlayer() { }
    }
}