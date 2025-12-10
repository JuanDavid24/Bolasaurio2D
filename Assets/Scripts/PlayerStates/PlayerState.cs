using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class PlayerState
    {
        protected Player _player;
        public PlayerState(Player player)
        {
            _player = player;
        }
        public virtual void HandleInput()
        {
            // x-axis movement
            _player.rb.velocity = new Vector2(_player.MovX * _player.WalkSpeed, _player.rb.velocity.y);
        }
        public virtual void PhysicsUpdate() { }
        public virtual void EnterState() { }
        public virtual void ExitState() { }

        public virtual void OnAttacked(int dmg, Vector2 enemyPos)
        {
            _player.hpManager.TakeDamage(dmg);
            _player.animator.SetTrigger("hurt");
            _player.rb.velocity = Vector2.zero;
            Vector2 dir = ((Vector2)_player.transform.position - enemyPos).normalized;
            //print("knockback vector: " + knockbackDir * 30f);
            _player.rb.velocity = dir * _player.KnockbackForce;
            _player.TransitionToState(new PlayerStateKnockback(_player));
            //_player.rb.AddForce(Vector2.up * _player.KnockbackForce, ForceMode2D.Impulse);
        }
    }
}