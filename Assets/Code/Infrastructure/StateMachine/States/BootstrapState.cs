using System;
using Code.Data.Configs;
using JetBrains.Annotations;
using UniRx;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        
        private readonly GlobalStateMachine _stateMachine;
        private GlobalData _data;
        private AsyncOperationHandle<SceneInstance> _sceneHandle;
        
        public BootstrapState( GlobalStateMachine stateMachine, GlobalData data)
        {
            _stateMachine = stateMachine;
            _data = data; 
        }
        public void Enter()
        {
          var operation =  Addressables.LoadSceneAsync(_data.MainMenu);
          operation.Completed += OnSceneChanged;
          _sceneHandle = operation;
        }

        private void OnSceneChanged(AsyncOperationHandle<SceneInstance> asyncOperationHandle)
        {
            _stateMachine.ChangeState<MenuState>();
        }

        public void Exit()
        {
            _sceneHandle.Completed -= OnSceneChanged;
        }
    }
}