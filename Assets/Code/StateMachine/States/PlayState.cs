using System;
using Code.StateMachine;

public class PlayState : IState
{
    ZombieServiceSpwaner _zombieService;

    public PlayState(ZombieServiceSpwaner zombieServiceSpawner)
    {
        _zombieService = zombieServiceSpawner;
    }
    public void Enter()
    {
        
    }

    public void ChangeState()
    {
        throw new NotImplementedException();
    }
}

public class ZombieServiceSpwaner
{
}