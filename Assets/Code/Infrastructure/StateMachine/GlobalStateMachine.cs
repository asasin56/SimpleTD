using System;
using System.Collections.Generic;
using Code.Infrastructure.StateMachine.States;
using Zenject;

namespace Code.Infrastructure.StateMachine
{
   public class GlobalStateMachine 
   {
      private readonly Dictionary<Type, Func<IState>> _stateFactories = new Dictionary<Type, Func<IState>>();
      private DiContainer _container;
      private IState _currentState;

      public GlobalStateMachine(DiContainer container)
      {
         _container = container;
      }
      
      public void RegisterState<T>() where T : IState
      {
         if (!_stateFactories.ContainsKey(typeof(T))) 
            _stateFactories[typeof(T)] = () => _container.Resolve<T>();
      }

      public void ChangeState<T>() where T : IState
      {
         _currentState?.Exit();

         if (!_stateFactories.TryGetValue(typeof(T), out var factory))
            throw new Exception($"State {typeof(T)} is not registered!");

         _currentState = factory.Invoke();
         _currentState.Enter();
      }
   }
}