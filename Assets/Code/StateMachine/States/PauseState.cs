using UnityEngine;

namespace Code.StateMachine.States
{
    public class PauseState : MonoBehaviour, IState
    {
        [SerializeField] private GameObject _pauseMenu;
        public void Enter()
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            Debug.Log("Pause");
        }

        public void ChangeState()
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
        }
    }
}