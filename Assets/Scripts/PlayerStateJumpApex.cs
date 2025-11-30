using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerStateJumpApex : PlayerState
    {
        public PlayerStateJumpApex(Player player) : base(player) { }

        public override void EnterState()
        {
            _player.rb.gravityScale = _player.DefaultGravity / 2;
        }

        public override void HandleInput()
        {
            //// detect landing
            if (_player.IsGrounded)
            {
                _player.TransitionToState(new PlayerStateGrounded(_player));
                return;
            }

            // fall
            if (Input.GetKeyUp(KeyCode.Space) || IsFalling)
            {
                _player.TransitionToState(new PlayerStateFalling(_player));
                return;
            }
        }

        private bool IsFalling => _player.rb.velocity.y < -_player.JumpHangTimeThreshold;
    }
}