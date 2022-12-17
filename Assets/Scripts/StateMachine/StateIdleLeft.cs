using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdleLeft : StateBase {
    public StateIdleLeft(StateMachine stateMachine) : base(stateMachine) {
        this.Criteria = new Dictionary<string, StateParameterCriteria>() {
            {"Direction", new StateParameterCriteria("Direction", new Vector2(-1, 0), StateParameterChecker.CompareTypes.Equal)}
        };
    }

    public override void OnEnter() {
        base.OnEnter();

        Debug.Log($"Enter Idle_Left");
    }
}
