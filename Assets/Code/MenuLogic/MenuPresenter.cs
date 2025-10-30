using System.Collections;
using System.Collections.Generic;
using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuPresenter
{
    MenuUIView  _view;
    MenuData _data;
    GlobalStateMachine  _stateMachine;

    public MenuPresenter(MenuUIView view, MenuData data,  GlobalStateMachine stateMachine)
    {
        _view = view;
        _data = data; 
        _stateMachine = stateMachine;
    } 

    public void Initialize()
    {
        _view.SubscribeButtons();
        
        _view.OnLinkClick
            .Where(i => i < _data.Links.Count) 
            .Subscribe(i => Application.OpenURL(_data.Links[i]))
            .AddTo(_view);
        
        _view.OnPlayClick
            .Subscribe(_ => OnPlayButtonClicked())
            .AddTo(_view);

        _view.OnQuitClick
            .Subscribe(_ => OnQuitButtonClicked())
            .AddTo(_view);
    }

    private void OnPlayButtonClicked() => _stateMachine.ChangeState<LoadingState>();

    private void OnQuitButtonClicked() => _stateMachine.ChangeState<ExitState>();
    
}