using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerStateKnockback : PlayerState
    {
        public PlayerStateKnockback(Player player) : base(player) { }
        public override void EnterState() 
        {
            _player.animator.SetFloat("xVelocity", 0);
            _player.animator.SetTrigger("hurt");
        }
        public override void HandleInput() { }
        public override void PhysicsUpdate() 
        {
            Debug.Log($"estado knockback, velocity {_player.rb.velocity.magnitude}");
            if (_player.rb.velocity.magnitude <= .01f)
            {
                // algo en el animator?
                _player.TransitionToState(new PlayerStateGrounded(_player));
            }
        }
        public override void OnAttacked(int dmg, Vector2 enemyPos) { }
    }
}