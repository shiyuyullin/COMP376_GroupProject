using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine: MonoBehaviour
{
    private IState currentlyRunningState;
    private IState previousState;

    public void changeState(IState newState)
    {
        if(currentlyRunningState != null)
        {
            currentlyRunningState.Exit();
        }
        previousState = currentlyRunningState;
        currentlyRunningState = newState;
        currentlyRunningState.Enter();
    }

    public void execuateStateUpdate()
    {
        var runningState = currentlyRunningState;
        if(runningState != null)
        {
            runningState.Execuate();
        }
    }

    public void SwitchToPreviousState()
    {
        currentlyRunningState.Exit();
        currentlyRunningState = previousState;
        currentlyRunningState.Enter();
    }
}
