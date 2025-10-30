using UnityEngine;

namespace Code.Infrastructure
{
    [CreateAssetMenu(fileName = "Global", menuName = "Global")]
    public class GlobalData : ScriptableObject
    {
        public string LoadingScene => _loadingScene;
        public string MainMenu => _mainMenu;

     [SerializeField]   private string _loadingScene;
       [SerializeField] private string _mainMenu;
    }
}