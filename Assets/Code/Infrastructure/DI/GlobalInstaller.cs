using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using Zenject;

namespace Code.Infrastructure.DI
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallGlobalServices();
        }

        private void InstallGlobalServices()
        {
            Container.Bind<GlobalStateMachine>()
                .AsSingle()
                .NonLazy();

            Container.Bind<IState>()
                .To<BootstrapState>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IState>()
                .To<MenuState>()
                .AsSingle()
                .NonLazy();

        }
    }
}