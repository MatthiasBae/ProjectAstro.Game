using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public abstract class StateBase {
    public bool IsRootState;
    public StateMachine StateMachine;
    
    public float AnimationSpeed;
    public StateAnimation Animation;
    
    public Dictionary<string, StateBase> SubStates;
    public Dictionary<string, StateParameterCriteria> Criteria;

    public StateBase CurrentSuperState;
    public StateBase CurrentSubState;

    public event Action Enter;
    public event Action Update;
    public event Action Exit;

    public StateBase(StateMachine stateMachine) {
        this.SubStates = new Dictionary<string, StateBase>();
        this.Criteria = new Dictionary<string, StateParameterCriteria>();
        this.StateMachine = stateMachine;
    }

    public virtual void OnEnter() {
        this.Enter?.Invoke();
    }
    public virtual void OnUpdate() {
        this.CurrentSubState?.OnUpdate();
        this.Update?.Invoke();
    }
    public virtual void OnExit() {
        this.Exit?.Invoke();
    }
    
    public void CheckSwitchState() {
        var allParameterMet = true;

        foreach(var state in this.SubStates.Values) {
            foreach(var criteria in state.Criteria) {
                var stateMachineParameters = this.StateMachine.Parameters;
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

    public virtual void SwitchState(StateBase state) {
        this.OnExit();

        state.OnEnter();
        if(this.IsRootState) {
            this.StateMachine.SwitchState(state);
        }
        else if(this.CurrentSuperState != null) {
            this.CurrentSuperState.SetSubState(state);
        }
    }


    public void SetSuperState(StateBase superState) {
        this.CurrentSuperState = superState;
    }

    public void SetSubState(StateBase subState) {
        this.CurrentSubState = subState;
        subState.SetSuperState(this);
    }

    public void AddSubState(string name, StateBase state) {
        this.SubStates[name] = state;
    }

    public void RemoveSubState(string name) {
        this.SubStates.Remove(name);
    }

    public StateBase GetSubState(string name) {
        return this.SubStates[name];
    }

    public void AddParameter(string name, StateParameterCriteria criteria) {
        this.Criteria.Add(name, criteria);
    }

}