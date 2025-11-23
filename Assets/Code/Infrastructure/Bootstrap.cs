using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        private GlobalStateMachine _stateMachine;

        [Inject]
        public void Constructor(GlobalStateMachine machine) => _stateMachine = machine;

        private void Start()
        {
            
            _stateMachine.RegisterState<BootstrapState>();
            _stateMachine.RegisterState<MenuState>();
            _stateMachine.RegisterState<LoadingState>();
            _stateMachine.RegisterState<GameState>();
            _stateMachine.RegisterState<ExitState>();
            
            _stateMachine.ChangeState<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        }
    }
}