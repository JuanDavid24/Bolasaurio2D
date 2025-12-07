using Assets.Scripts.EnemyStates;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyStates
{
    public class EnemyStateChase : EnemyState
    {
        private float playerDistance;

        public EnemyStateChase(EnemyController enemy, EnemyStateManager stateManager) : base(enemy, stateManager) { }

        public override void HandleState()
        {
            DetectPlayer();
            AnimateWalking();
            FlipSpriteAuto();
        }

        private void DetectPlayer()
        {
            playerDistance = Vector3.Distance(_player.position, _enemy.transform.position);

            if (playerDistance < _enemy.SightDistance)
            {
                MoveTowardsX(_player.position);

                if (playerDistance < _enemy.AttackDistance)
                {
                    // atacar
                    //_stateManager.SwitchState(_enemyStateAttack);
                }
            }
            else
            {
                _stateManager.TransitionToState(new EnemyStatePatrol(_enemy, _stateManager));
            }
        }
        private void MoveTowardsX(Vector2 target)
        {
            float direction = Mathf.Sign(target.x - _enemy.transform.position.x);
            _enemy.Rb.velocity = new Vector2(direction * _enemy.PatrolSpeed, _enemy.Rb.velocity.y);
        }

        private void FlipSpriteAuto()
        {
            float dir = Mathf.Sign(_enemy.Rb.velocity.x);
            if (dir != 0) 
            {
                Vector3 prevScale = _enemy.transform.localScale;
                _enemy.transform.localScale = new Vector3(dir * Mathf.Abs(prevScale.x), prevScale.y, prevScale.z);
            }
        }
    }
}