using Assets.Scripts.EnemyStates;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyStatePatrol : EnemyState
    {
        private Transform _pointA, _pointB, _currentTarget;
        private float playerDistance;

        public EnemyStatePatrol(EnemyController enemy, EnemyStateManager stateManager) : base(enemy, stateManager) { }

        public override void EnterState() 
        {
            base.EnterState();
            _pointA = _enemy.pointA;
            _pointB = _enemy.pointB;
            _currentTarget = _pointA;
        }
        public override void HandleState()
        {
            DetectPlayer();
            FlipSpriteAuto();

            float distanceToTarget = Vector2.Distance(_currentTarget.position, _enemy.transform.position);
            if (distanceToTarget < 0.5f)
            {
                _currentTarget = ToggleTarget;
            }
            MoveTowardsX(_currentTarget.position);
            AnimateWalking();
        }

        private void DetectPlayer()
        {
            playerDistance = Vector3.Distance(_player.position, _enemy.transform.position);

            if (playerDistance < _enemy.SightDistance)
            {
                _stateManager.TransitionToState(new EnemyStateChase(_enemy, _stateManager));
            }
        }
        private void MoveTowardsX(Vector2 target)
        {
            float direction = Mathf.Sign(target.x - _enemy.transform.position.x);
            _enemy.Rb.velocity = new Vector2(direction * _enemy.PatrolSpeed, _enemy.Rb.velocity.y);
        }

        private Transform ToggleTarget => _currentTarget == _pointA ? _pointB : _pointA;
    }
}