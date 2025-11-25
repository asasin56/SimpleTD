using System.Threading;
using Cysharp.Threading.Tasks;

namespace Code.GameplayLogic.Shop.Coins
{
    public class CoinsPresenter
    {
        private CoinsView _coinsView;
        private CoinsConfig  _coinsConfig;
        private int _coinsCount;
        
        private CancellationTokenSource _token;

        public CoinsPresenter(CoinsView coinsView, CoinsConfig coinsConfig)
        {
            _coinsView = coinsView;
            _coinsConfig = coinsConfig;
        }

        public void Initialize()
        { 
            _token = new CancellationTokenSource(); 
            _coinsView.DisplayAmount(_coinsCount); 
            var task = StartIncomeLoop();
        }
        

        public int GetCoinsCount() => _coinsCount;

        public void TakeCoins(int amount)
        {
            _coinsCount -= amount;
            _coinsView.DisplayAmount(_coinsCount);
        }
        public void StopLoop() => _token.Cancel();

        private async UniTaskVoid StartIncomeLoop()
        {
            while (!_token.IsCancellationRequested)
            {
                await UniTask
                    .Delay(_coinsConfig.IncreasingRate, DelayType.Realtime, 
                        cancellationToken: _token.Token);
                _coinsCount += _coinsConfig.IncreasingAmount;
                _coinsView.DisplayAmount(_coinsCount);
            }
        }
    }
}