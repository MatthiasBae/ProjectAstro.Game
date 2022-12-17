using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdleDown : StateBase {
    public StateIdleDown(StateMachine stateMachine) : base(stateMachine) {
        this.Criteria = new Dictionary<string, StateParameterCriteria>() {
            {"Direction", new StateParameterCriteria("Direction", new Vector2(0, -1), StateParameterChecker.CompareTypes.Equal)}
        };
    }

    public override void OnEnter() {
        base.OnEnter();

        Debug.Log($"Enter Idle_Down");
    }
}
