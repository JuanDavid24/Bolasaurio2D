using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyStates
{
    public class EnemyStateAttack : EnemyState
    {
        public EnemyStateAttack(EnemyController enemy, EnemyStateManager stateManager) : base(enemy, stateManager) { }

        public override void EnterState()
        {
            base.EnterState();
            _enemy.Rb.velocity = new Vector2(0, _enemy.Rb.velocity.y);
            FlipSpriteToPlayer();
            _enemy.Anim.SetTrigger("attack");
        }

        public override void PhysicsUpdate()
        {
            float dir = _enemy.transform.localScale.x;
            _enemy.Rb.velocity = new Vector2(_enemy.AttackAnimationVelocity * dir, _enemy.Rb.velocity.y);
        }
        public override void OnAnimationEnd()
        {
            _stateManager.TransitionToState(new EnemyStateCooldown(_enemy, _stateManager));
        }
    }
}