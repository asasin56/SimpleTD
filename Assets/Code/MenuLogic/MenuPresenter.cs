using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UniRx;
using UnityEngine;

namespace Code.MenuLogic
{
    public class MenuPresenter
    {
        private MenuUIView  _view;
        private MenuData _data;
        private GlobalStateMachine  _stateMachine;
        public MenuPresenter( MenuUIView view, MenuData data,  GlobalStateMachine stateMachine)
        {
            _view = view;
            _data = data; 
            _stateMachine = stateMachine;
        } 

        public void Initialize()
        {
            Debug.Log("Initialize MenuPresenter");
            _view.SubscribeButtons();
        
            _view.LinkClicked
                .Where(i => i < _data.Links.Count) 
                .Subscribe(i => Application.OpenURL(_data.Links[i]))
                .AddTo(_view);
        
            _view.PlayClicked
                .Subscribe(_ => OnPlayButtonClicked())
                .AddTo(_view);

            _view.QuitClicked
                .Subscribe(_ => OnQuitButtonClicked())
                .AddTo(_view);
        }

        private void OnPlayButtonClicked() => _stateMachine.ChangeState<LoadingState>();

        private void OnQuitButtonClicked() => _stateMachine.ChangeState<ExitState>();

 
    }
}