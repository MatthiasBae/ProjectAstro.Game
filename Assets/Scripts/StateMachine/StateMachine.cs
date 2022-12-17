using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {
    public Dictionary<string, StateParameter> Parameters;
    public Dictionary<string, StateBase> States;
    public StateBase CurrentState;

    public event Action<StateBase> StateChanged;

    public StateMachine() {
        this.Parameters = new Dictionary<string, StateParameter>();
    }

    public void SetStateLibrary(StateLibraryBase stateLibraryBase) {
        this.States = stateLibraryBase.States;
        this.CurrentState = stateLibraryBase.DefaultState;
        //@TODO: Default state should be set in the state library
    }

    public void AddParameter(string name, StateParameter parameter) {
        this.Parameters.Add(name, parameter);
    }

    public void SetParameter(string name, object value) {
        this.Parameters[name].Value = value;
    }

    public void Update() {
        Debug.Log(nameof(this.CurrentState));
        this.CheckSwitchState();
        this.CurrentState.OnUpdate();
    }

    public void CheckSwitchState() {
        var allParameterMet = true;
        
        foreach(var state in this.States.Values) {
            foreach(var criteria in state.Criteria) {
                var stateMachineParameters = this.Parameters;
                if(!stateMachineParameters.ContainsKey(criteria.Key)) {
                    return;
                } 
                var stateMachineParameter = stateMachineParameters[criteria.Key];
                
                if(!stateMachineParameter.Compare(criteria.Value)) {
                    allParameterMet = false;
                    break;
                }
            }

            if(allParameterMet) {
                this.SwitchState(state);
                break;
            }
        }
    }

    public void SwitchState(StateBase state) {
        if(this.CurrentState != null) {
            this.CurrentState.OnExit();
        }

        if(this.CurrentState == state) {
            return;
        }

        this.CurrentState = state;
        this.StateChanged?.Invoke(state);
        this.CurrentState.OnEnter();
    }
}
