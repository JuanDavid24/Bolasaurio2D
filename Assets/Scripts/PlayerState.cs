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
        public abstract void HandleInput();
        public virtual void EnterState() { }
        public virtual void ExitState() { }
    }
}