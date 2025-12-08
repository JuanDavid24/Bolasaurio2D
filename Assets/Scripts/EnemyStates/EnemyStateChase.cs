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
            FlipSpriteToPlayer();
            DetectPlayer();
            AnimateWalking();
        }

        private void DetectPlayer()
        {
            playerDistance = Vector3.Distance(_player.position, _enemy.transform.position);

            if (playerDistance < _enemy.SightDistance)
            {

                if (playerDistance < _enemy.AttackDistance)
                {
                    // atacar
                    _stateManager.TransitionToState(new EnemyStateAttack(_enemy, _stateManager));
                    return;
                }
                
                MoveTowardsX(_player.position);
            }
            else
            {
                _stateManager.TransitionToState(new EnemyStatePatrol(_enemy, _stateManager));
            }
        }
        //public override void PhysicsUpdate()
        //{

        //}

        private void MoveTowardsX(Vector2 target)
        {
            float direction = Mathf.Sign(target.x - _enemy.transform.position.x);
            _enemy.Rb.velocity = new Vector2(direction * _enemy.PatrolSpeed, _enemy.Rb.velocity.y);
        }
    }
}