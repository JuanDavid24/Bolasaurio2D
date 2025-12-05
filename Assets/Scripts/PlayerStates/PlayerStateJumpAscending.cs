using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerStateJumpAscending : PlayerState
    {
        public PlayerStateJumpAscending(Player player) : base(player) { }

        public override void EnterState()
        {
            _player.rb.velocity += new Vector2(_player.rb.velocity.x, _player.JumpForce);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            //// detect landing
            //if (_player.IsGrounded)
            //{
            //    _player.TransitionToState(new PlayerStateGrounded(_player));
            //    return;
            //}

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _player.TransitionToState(new PlayerStateFalling(_player, 3.5f));
                return;
            }
            if (ReachedJumpApex)
            {
                _player.TransitionToState(new PlayerStateJumpApex(_player));
            }
        }

        private bool ReachedJumpApex => _player.rb.velocity.y < _player.JumpHangTimeThreshold;
    }

}
