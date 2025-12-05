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
