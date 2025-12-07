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
        }
        public virtual void EnterState()
        {
            _player = _enemy.player;
        }
        public abstract void HandleState();
        public virtual void PhysicsUpdate() { }
        public virtual void ExitState() { }
        public virtual void OnAttacked() { }
        public virtual void OnCollisionWithPlayer() { }
        protected virtual void AnimateWalking()
        {
            float isWalking = _enemy.Rb.velocity.x != 0 ? 1 : 0;
            _enemy.Anim.SetFloat("xVelocity", isWalking);
        }

        protected virtual void FlipSpriteAuto()
        {
            float dir = Mathf.Sign(_enemy.Rb.velocity.x);
            if (dir != 0)
            {
                Vector3 prevScale = _enemy.transform.localScale;
                _enemy.transform.localScale = new Vector3(dir * Mathf.Abs(prevScale.x), prevScale.y, prevScale.z);
            }
        }
    }
}