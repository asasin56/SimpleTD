using System;

namespace Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {

        private GlobalStateMachine _stateMachine;
        
        public BootstrapState(GlobalStateMachine stateMachine)
        {
            _stateMachine = stateMachine; 
        }
        public void Enter()
        {
            
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}