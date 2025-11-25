using System;
using Code.GameplayLogic.Shop.Coins;
using UnityEngine;
using Zenject;

namespace Code.GameplayLogic
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private CoinsPresenter _coinsPresenter;
        
        [Inject]
        public void Constructor(CoinsPresenter coinsPresenter)
        {
            _coinsPresenter = coinsPresenter;
        }

        private void Awake()
        {
            _coinsPresenter.Initialize();
        }
    }
}