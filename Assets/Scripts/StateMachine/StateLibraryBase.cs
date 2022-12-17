using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLibraryBase {
    public Dictionary<string, StateBase> States;
    public StateBase DefaultState;
    public StateLibraryBase(StateMachine stateMachine) {
        this.States = new Dictionary<string, StateBase>();
    }
}
