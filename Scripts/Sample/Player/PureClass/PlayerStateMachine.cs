using TettekeKobo.StateMachine;
using UnityEngine;

namespace TettekeKobo.StatePatternTest
{
    /// <summary>
    /// プレイヤーのStateMachine(Interfaceを用いたパターン)
    /// </summary>
    public class PlayerStateMachine : IHamuStateMachine<PlayerStateType>,ITransitionState<PlayerStateType>
    {
        private IState currentState;
        
        private readonly IdleState idleState;
        private readonly WalkState walkState;
        private readonly JumpState jumpState;

        public PlayerStateMachine()
        {
            idleState = new IdleState(this);
            walkState = new WalkState(this);
            jumpState = new JumpState(this);
        }


        public void Initialize(PlayerStateType stateType)
        {
            var startState = ConvertToState(stateType);
            currentState = startState;
            currentState.Enter();
        }

        public void MyUpdate()
        {
            currentState?.MyUpdate();
        }

        public void MyFixedUpdate()
        {
            currentState?.MyFixedUpdate();
        }

        public void TransitionState(PlayerStateType stateType)
        {
            currentState.Exit();
            var newState = ConvertToState(stateType);
            currentState = newState;
            currentState.Enter();
        }

        public IState ConvertToState(PlayerStateType stateType)
        {
            switch (stateType)
            {
                case PlayerStateType.Idle:
                    return idleState;
                case PlayerStateType.Walk:
                    return walkState;
                case PlayerStateType.Jump:
                    return jumpState;
                default:
                    Debug.LogWarning("存在しないStateが渡されました");
                    return null;
            }
        }
    }
}