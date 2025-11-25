using System.Collections;
using System.Collections.Generic;
using Code.Data.Configs;
using Code.Infrastructure;
using Code.Infrastructure.StateMachine.States;
using Code.MenuLogic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private MenuData _menuData;
    [SerializeField] private MenuUIView _ui;

    public override void InstallBindings()
    {
        Container.Bind<MenuPresenter>()
            .AsSingle()
            .NonLazy();

        Container.Bind<MenuUIView>()
            .FromInstance(_ui)
            .AsSingle()
            .NonLazy();
        
        Container.Bind<GlobalData>()
            .AsSingle()
            .NonLazy();
        
        Container.Bind<MenuData>()
            .FromInstance(_menuData)
            .AsSingle()
            .NonLazy();

    }
}
