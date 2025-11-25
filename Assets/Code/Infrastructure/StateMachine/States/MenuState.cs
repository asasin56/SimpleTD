using System;
using Code.Data.Configs;
using UniRx;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        
        private GlobalData _globalData;
        
        public MenuState(GlobalData globalData)
        {
            _globalData = globalData;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            SceneManager.LoadScene(_globalData.LoadingScene);
        }
    }
}