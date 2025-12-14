using Assets.Scripts.EnemyStates;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyStatePatrol : EnemyState
    {
        private Transform _pointA, _pointB, _currentTarget;
        private float playerDistance;
        private PatrolEnemyController _patrolEnemy;

        public EnemyStatePatrol(PatrolEnemyController enemy, EnemyStateManager stateManager) : base(enemy, stateManager) 
        {
            _patrolEnemy = enemy;
        }

        public override void EnterState() 
        {
            base.EnterState();
            _pointA = _patrolEnemy.pointA;
            _pointB = _patrolEnemy.pointB;
            _currentTarget = _pointA;
        }
        public override void HandleState()
        {
            //DetectPlayer();
            if (_patrolEnemy.IsPlayerOnSight)
            {
                _stateManager.TransitionToState(new EnemyStateChase(_patrolEnemy, _stateManager));
            }

            _enemy.FlipSpriteAuto();

            float distanceToTarget = Vector2.Distance(_currentTarget.position, _enemy.transform.position);
            if (distanceToTarget < 0.5f)
            {
                _currentTarget = ToggleTarget;
            }
            _patrolEnemy.MoveTowardsX(_currentTarget.position, _patrolEnemy.PatrolSpeed);
            _enemy.AnimateMovement();
        }

        private Transform ToggleTarget => _currentTarget == _pointA ? _pointB : _pointA;
    }
}