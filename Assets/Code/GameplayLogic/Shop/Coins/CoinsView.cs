using Code.MenuLogic;
using TMPro;
using UnityEngine;

namespace Code.GameplayLogic.Shop.Coins
{
    public class CoinsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public void DisplayAmount(int amount) => 
            _text.text = amount.ToString();
    }
}