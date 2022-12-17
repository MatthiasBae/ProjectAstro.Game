using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : StateBase {
    public StateIdle(StateMachine stateMachine) : base(stateMachine) {
        this.IsRootState = true;
        this.Criteria = new Dictionary<string, StateParameterCriteria>() {
            {"Speed", new StateParameterCriteria("Speed", 0, StateParameterChecker.CompareTypes.Equal)}
        };

        this.SubStates = new Dictionary<string, StateBase> {
            {"Idle_Up", new StateIdleUp(stateMachine) },
            {"Idle_Down", new StateIdleDown(stateMachine) },
            {"Idle_Right", new StateIdleRight(stateMachine) },
            {"Idle_Left", new StateIdleLeft(stateMachine) },
        };
    }

    public override void OnEnter() {
        base.OnEnter();

        Debug.Log($"Enter Idle");
    }
}
