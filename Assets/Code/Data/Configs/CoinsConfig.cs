using UnityEngine;

namespace Code.GameplayLogic.Shop.Coins
{
    [CreateAssetMenu(fileName = "Coins", menuName = "Configs/Coins")]
    public class CoinsConfig : ScriptableObject
    {
        public int IncreasingRate => _increasingRate;
        public int IncreasingAmount => _increasingAmount; 

        [SerializeField] private int _increasingRate;
        [SerializeField] private int _increasingAmount;
    }
}