using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateLibraryHuman : StateLibraryBase {
    public StateLibraryHuman(StateMachine stateMachine) : base(stateMachine) {
        var stateIdle = new StateIdle(stateMachine);

        this.States = new Dictionary<string, StateBase>() {
            {"Idle", stateIdle },
            {"Walk", null },
            {"Run", null }
        };

        this.DefaultState = stateIdle;
    }
}
