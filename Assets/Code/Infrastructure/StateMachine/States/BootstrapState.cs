using System;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        
        private readonly GlobalStateMachine _stateMachine;
        private GlobalData _data;
        
        public BootstrapState( GlobalStateMachine stateMachine, GlobalData data)
        {
            _stateMachine = stateMachine;
            _data = data; 
        }
        public void Enter()
        {
            SceneManager.LoadSceneAsync(_data.MainMenu);
            SceneManager.sceneLoaded += OnSceneChanged; 
        }

        private void OnSceneChanged(Scene scene, LoadSceneMode mode)
        {
            _stateMachine.ChangeState<MenuState>();
        }

        public void Exit()
        {
            SceneManager.sceneLoaded -= OnSceneChanged;
        }
    }
}