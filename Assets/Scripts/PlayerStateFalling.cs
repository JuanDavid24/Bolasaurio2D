using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Assets.Scripts
{
    public class PlayerStateFalling : PlayerState
    {
        private float _gravityMultiplier;
        public PlayerStateFalling(Player player, float gravityMultiplier = 2) : base(player) 
        { 
            _gravityMultiplier = gravityMultiplier;
        }

        public override void EnterState()
        {
            _player.rb.gravityScale = _player.DefaultGravity * _gravityMultiplier;
        }

        public override void HandleInput()
        {
            LimitFallGravity();
            
            // detect landing
            if (_player.IsGrounded)
            {
                _player.TransitionToState(new PlayerStateGrounded (_player));
            }
        }

        private void LimitFallGravity()
        {
            _player.rb.velocity = new Vector2(_player.rb.velocity.x, Mathf.Max(_player.rb.velocity.y, -_player.MaxFallVelocity));
        }
    }
}