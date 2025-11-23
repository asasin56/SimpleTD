using UnityEngine.SceneManagement;

namespace Code.Infrastructure.StateMachine.States
{
    public class LoadingState : IState
    {
        private GlobalStateMachine _stateMachine;
        private GlobalData _data;
        public LoadingState(GlobalStateMachine machine, GlobalData data)
        {
            _stateMachine = machine;
            _data = data;
        }
        public void Enter()
        {
         var action =   SceneManager.LoadSceneAsync(_data.GameScene);
         action.completed += _ => Exit();
        }

        public void Exit()
        {
            _stateMachine.ChangeState<GameState>();
        }
    }
}