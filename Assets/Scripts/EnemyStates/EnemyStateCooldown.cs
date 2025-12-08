using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyStates
{
    public class EnemyStateCooldown : EnemyState
    {
        Timer _timer;
        public EnemyStateCooldown(EnemyController enemy, EnemyStateManager stateManager) : base(enemy, stateManager) { }
        
        public override void EnterState()
        {
            base.EnterState();
            _enemy.Rb.velocity = new Vector2(0, _enemy.Rb.velocity.y);
            _timer = _enemy.TimerCooldown;
            _timer.timeLeft = _enemy.AttackCooldown;
            _timer.timerOn = true;
        }

        public override void HandleState()
        {
            if (!_timer.timerOn)
            {
                _stateManager.TransitionToState(new EnemyStatePatrol(_enemy, _stateManager));
            }
        }
    }
}