
using UnityEngine;

namespace Code.Infrastructure.StateMachine.States
{
    public class ExitState :  IState
    {
        public void Enter() => Exit();

        public void Exit() => Application.Quit();
    }
}
