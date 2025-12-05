using Assets.Scripts.EnemyStates;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyStatePatrol : EnemyState
    {
        private Transform _player, _pointA, _pointB, _currentTarget;
        private float _sightDistance = 10f;
        public EnemyStatePatrol(EnemyController enemy, EnemyStateManager stateManager) : base(enemy, stateManager) { }

        public override void EnterState()
        {
            _pointA = _enemy.pointA;
            _pointB = _enemy.pointB;
            _currentTarget = _pointA;
        }
        public override void HandleState()
        {
            float distanceToTarget = Vector2.Distance(_currentTarget.position, _enemy.transform.position);
            //Debug.Log("distancia "+ distanceToTarget);
            if (distanceToTarget < 0.5f)
            {
                _currentTarget = ToggleTarget;
                //Debug.Log("current target " + _currentTarget);
                FlipSprite();
            }
            MoveTowardsX(_currentTarget.position);
        }
        private Transform ToggleTarget => _currentTarget == _pointA ? _pointB : _pointA;
        private void MoveTowardsX(Vector2 target)
        {
            float direction = Mathf.Sign(target.x - _enemy.transform.position.x);
            _enemy.Rb.velocity = new Vector2(direction * _enemy.PatrolSpeed, _enemy.Rb.velocity.y);
        }

        private void FlipSprite() 
        {
            Vector3 prevScale = _enemy.transform.localScale;
            _enemy.transform.localScale = new Vector3(-prevScale.x, prevScale.y, prevScale.z);
        }
    }
}