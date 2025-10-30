using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MenuUIView : MonoBehaviour
{
   [SerializeField] private List<Button> _linkButtons = new List<Button>();
   [SerializeField] private Button _playButton;
   [SerializeField] private Button _quitButton;
    
   
    public readonly Subject<int> OnLinkClick = new Subject<int>();
    public readonly Subject<Unit> OnPlayClick = new Subject<Unit>();
    public readonly Subject<Unit> OnQuitClick = new Subject<Unit>();
    public void SubscribeButtons()
    {
        for (int i = 0; i < _linkButtons.Count; i++)
        {
            int index = i; 
            _linkButtons[i].onClick.AsObservable()
                .Subscribe(_ => OnLinkClick.OnNext(index))
                .AddTo(this);
        }

        _playButton.onClick.AsObservable().Subscribe(_ => OnPlayClick.OnNext(Unit.Default)).AddTo(this);
        _quitButton.onClick.AsObservable().Subscribe(_ => OnQuitClick.OnNext(Unit.Default)).AddTo(this);
        
    }
    
    private void OnDestroy()
    {
        OnLinkClick.OnCompleted();
        OnPlayClick.OnCompleted();
        OnQuitClick.OnCompleted();
    }
}
