using Code.Data.Configs;
using Code.GameplayLogic.Shop;
using Code.GameplayLogic.Shop.Coins;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.DI
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private ShopView _shopView;
        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private ShopConfig _shopConfig;
        [SerializeField] private ZombieBehaviourConfig  _zombieConfig;
        [SerializeField] private CoinsConfig _coinsConfig;

        public override void InstallBindings()
        {
            Container.Bind<ShopView>()
                .FromInstance(_shopView)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<CoinsView>()
                .FromInstance(_coinsView)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<ShopConfig>()
                .FromInstance(_shopConfig)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<CoinsConfig>()
                .FromInstance(_coinsConfig)
                .AsSingle()
                .NonLazy();

            
            Container.Bind<ZombieBehaviourConfig>()
                .FromInstance(_zombieConfig)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<CoinsPresenter>()
                .AsSingle()
                .NonLazy();
            
        }
    }
}