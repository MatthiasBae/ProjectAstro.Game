using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateParameterCriteria {
    public string Name;
    public object Value;
    public StateParameterChecker.CompareTypes Type;

    public StateParameterCriteria(string name, object value, StateParameterChecker.CompareTypes type) {
        this.Name = name;
        this.Value = value;
        this.Type = type;
    }
}
