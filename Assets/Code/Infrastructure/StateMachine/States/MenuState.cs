using UnityEngine.SceneManagement;

namespace Code.Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        private MenuPresenter _presenter;
        private GlobalData _globalData;

        public MenuState(MenuPresenter presenter, GlobalData globalData)
        {
            _presenter = presenter;
            _globalData = globalData;
        }
        
        public void Enter()
        {
            _presenter.Initialize();
        }

        public void Exit()
        {
            SceneManager.LoadScene(_globalData.LoadingScene);
        }
    }
}