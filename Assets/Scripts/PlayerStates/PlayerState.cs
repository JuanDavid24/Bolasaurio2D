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
    }
}