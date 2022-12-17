using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateParamaterLibraryHuman : StateParamaterLibraryBase {
    public StateParamaterLibraryHuman() {
        this.Parameters = new Dictionary<string, StateParameter> {
            {"Speed", new StateParameter("Speed", 0) },
            {"Direction", new StateParameter("Direction", new Vector2(0,0)) }
        };
    }
}
