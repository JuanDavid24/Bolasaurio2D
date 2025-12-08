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
        public virtual void EnterState() {
            //Debug.Log($"Enter State: ${_stateManager.CurrentState}");
        }
        public virtual void HandleState() { }
        public virtual void PhysicsUpdate() { }
        public virtual void ExitState() { }
        public virtual void OnAttacked() { }
        public virtual void OnCollisionWithPlayer() { }
        public virtual void OnAnimationEnd() { }
        protected virtual void AnimateWalking()
        {
            float isWalking = _enemy.Rb.velocity.x != 0 ? 1 : 0;
            _enemy.Anim.SetFloat("xVelocity", isWalking);
        }

        protected void FlipSpriteAuto()
        {
            float dir = Mathf.Sign(_enemy.Rb.velocity.x);
            if (dir != 0)
            {
                Vector3 prevScale = _enemy.transform.localScale;
                _enemy.transform.localScale = new Vector3(dir * Mathf.Abs(prevScale.x), prevScale.y, prevScale.z);
            }
            //Debug.Log($"FlipSpriteAuto: {_enemy.transform.localScale.x}");
        }

        protected void FlipSpriteToPlayer()
        {
            float directionToPlayer = _player.position.x - _enemy.transform.position.x;
            float distanceToPlayer = Mathf.Abs(directionToPlayer);
            if (distanceToPlayer > 0.1f)
            {
                Vector3 prevScale = _enemy.transform.localScale;
                float dir = Mathf.Sign(directionToPlayer);
                _enemy.transform.localScale = new Vector3(dir * Mathf.Abs(prevScale.x), prevScale.y, prevScale.z);
            }
            //Debug.Log($"FlipSpriteToPlayer: {_enemy.transform.localScale.x}");
        }
    }
}