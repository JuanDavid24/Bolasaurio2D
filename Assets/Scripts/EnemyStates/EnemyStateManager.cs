using UnityEngine;

namespace Assets.Scripts.EnemyStates
{
    public class EnemyStateManager
    {
        public EnemyState CurrentState { get; private set; }

        public void Initialize(EnemyState initialState)
        {
            CurrentState = initialState;
            CurrentState.EnterState();
        }

        public void TransitionToState(EnemyState newState)
        {
            if (CurrentState != newState)
            {
                CurrentState.ExitState();
                CurrentState = newState;
                CurrentState.EnterState();
            }
            //Debug.Log($"Cambio de state: ${CurrentState}");
        }

        public void UpdateState()
        {
            CurrentState?.HandleState();
        }

        public void FixedUpdateState()
        {
            CurrentState?.PhysicsUpdate();
        }
    }
}
