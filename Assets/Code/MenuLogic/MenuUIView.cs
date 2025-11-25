using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MenuUIView : MonoBehaviour
{
    
   public IObservable<int> LinkClicked => _linkClicked;
   public IObservable<Unit> PlayClicked => _playClicked;
   public IObservable<Unit> QuitClicked => _quitClicked;
   
   [SerializeField] private List<Button> _linkButtons = new();
   [SerializeField] private Button _playButton;
   [SerializeField] private Button _quitButton;
    
   private readonly Subject<int> _linkClicked = new();
   private readonly Subject<Unit> _playClicked = new();
   private readonly Subject<Unit> _quitClicked = new();

   private CompositeDisposable _disposables = new();
   
    public void SubscribeButtons()
    {
        _disposables.Clear();
        for (int i = 0; i < _linkButtons.Count; i++)
        {
            int index = i; 
            _linkButtons[i].onClick.AsObservable()
                .Subscribe(_ => _linkClicked.OnNext(index))
                .AddTo(_disposables);
        }

        _playButton.onClick.AsObservable()
            .Subscribe(_ => _playClicked.OnNext(Unit.Default)).AddTo(_disposables);
        _quitButton.onClick.AsObservable()
            .Subscribe(_ => _quitClicked.OnNext(Unit.Default)).AddTo(_disposables);
        
    }

    private void OnDestroy()
    {
        _linkClicked.Dispose();
        _playClicked.Dispose();
        _quitClicked.Dispose();
    }
}
