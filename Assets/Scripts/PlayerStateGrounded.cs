using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerStateGrounded : PlayerState
    {
        public PlayerStateGrounded(Player player) : base(player) { }

        // Use this for initialization
        public override void EnterState()
        {
            _player.rb.gravityScale = _player.DefaultGravity;
        }
        public override void HandleInput() 
        {
            if (!_player.IsGrounded)
            {
                _player.TransitionToState(new PlayerStateFalling (_player));
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _player.TransitionToState(new PlayerStateJumpAscending(_player));
            }
        }
    }
}