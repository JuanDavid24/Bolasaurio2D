using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
            //print("enemypos " + enemyPos);

            _player.animator.SetTrigger("hurt");
            Vector2 knockbackDir = (new Vector2(_player.transform.position.x, _player.transform.position.y) - enemyPos).normalized;
            //print("knockback vector: " + knockbackDir * 30f);
            _player.rb.velocity = Vector2.zero;
            _player.rb.AddForce(Vector2.up * _player.KnockbackForce, ForceMode2D.Impulse);
        }
    }
}