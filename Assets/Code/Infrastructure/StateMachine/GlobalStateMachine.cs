using System;
using System.Collections.Generic;
using Code.Infrastructure.StateMachine.States;

namespace Code.Infrastructure.StateMachine
{
   public class GlobalStateMachine 
   {
      private readonly Dictionary<Type, IState> _stateDictionary = new();
      private IState _currentState;

      public GlobalStateMachine(List<IState> states)
      {
         foreach (var state in states)
         {
            _stateDictionary.Add(state.GetType(), state);
         }
      }
      public void ChangeState<T>() where T : IState
      {
         _stateDictionary[typeof(T)].Enter();
         _currentState.Exit();
         _currentState = _stateDictionary[typeof(T)];
      }
      
   }
}