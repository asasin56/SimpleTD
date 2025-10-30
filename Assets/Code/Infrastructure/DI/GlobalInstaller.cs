using System;
using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.DI
{

    public class GlobalInstaller : MonoInstaller
    { 
        [SerializeField] private GlobalData _globalData;
        public override void InstallBindings()
        {
            Debug.Log("[GlobalInstaller] InstallBindings CALLED ✅");
            InstallGlobalServices();
        }

        private void InstallGlobalServices()
        {
            Container.Bind<GlobalStateMachine>()
                .AsSingle()
                .NonLazy();

            Container.Bind<BootstrapState>()
                .AsTransient()
                .NonLazy();

            Container.Bind<MenuState>()
                .AsTransient()
                .Lazy();

            Container.Bind<GameState>()
                .AsTransient()
                .NonLazy();
            
            Container.Bind<LoadingState>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<GlobalData>()
                .FromInstance(_globalData)
                .AsSingle()
                .NonLazy();

        }
    }
}