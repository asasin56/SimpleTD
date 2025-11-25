using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "Global", menuName = "Configs/Global")]
    public class GlobalData : ScriptableObject
    {
        public string LoadingScene => _loadingScene;
        public string MainMenu => _mainMenu;
        public string GameScene => _gameScene; 

        [SerializeField]   private string _loadingScene;
       [SerializeField] private string _mainMenu;
       [SerializeField] private string _gameScene;
    }
}