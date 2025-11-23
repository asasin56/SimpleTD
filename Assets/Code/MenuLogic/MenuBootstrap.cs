using System;
using UnityEngine;
using Zenject;

namespace Code.MenuLogic
{
    public class MenuBootstrap : MonoBehaviour
    {
        private MenuPresenter  _presenter;
        
        [Inject]
        private void Constructor(MenuPresenter  presenter) => 
            _presenter = presenter;

        private void Awake() => _presenter.Initialize();
    }
}