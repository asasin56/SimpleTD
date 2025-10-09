using System.Collections;
using System.Collections.Generic;
using Code.StateMachine;
using Code.StateMachine.States;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
   private List<IState> _states = new List<IState>() { new PauseState() };
   private IState _currentState;
   private void Start()
   {
      _currentState = new PlayState(new ZombieServiceSpwaner());
   }
}