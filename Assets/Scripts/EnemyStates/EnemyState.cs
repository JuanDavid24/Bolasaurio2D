using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyStates
{
    public abstract class EnemyState
    {
        protected EnemyStateManager _stateManager;
        protected EnemyController _enemy;
        protected Transform _player;
        public EnemyState(EnemyController enemy, EnemyStateManager stateManager)
        {
            _enemy = enemy;
            _stateManager = stateManager;
            _player = _enemy.player;
        }
        public virtual void EnterState() { }
        public virtual void HandleState() { }
        public virtual void PhysicsUpdate() { }
        public virtual void ExitState() { }
        public virtual void OnAttacked(int dmg, Vector2 dmgDealerPos) 
        {
            HpManager hpm = _enemy.gameObject.GetComponent<HpManager>();
            hpm.TakeDamage(dmg);
            _enemy.Anim.SetTrigger("hurt");
        }
        public virtual void OnCollisionWithPlayer() { }
        public virtual void OnAnimationEnd() { }
    }
}