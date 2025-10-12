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
            _stateMachine.ChangeState<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}