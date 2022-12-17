using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateParameter {
    public string Name;
    public object Value;
    
    public StateParameterChecker Checker;

    public StateParameter(string name, object value) {
        this.Name = name;
        this.Value = value;
        this.Checker = new StateParameterChecker();
    }
    
    public bool Compare(StateParameterCriteria criteria) {
        return this.Checker.Compare(this, criteria);
    }

    public bool IsEqual(object value) {
        return this.Checker.IsEqual(this, value);
    }

    public bool IsGreaterThan(object value) {
        return this.Checker.IsGreaterThan(this, value);
    }

    public bool IsLessThan(object value) {
        return this.Checker.IsLessThan(this, value);
    }

    public bool IsGreaterThanOrEqualTo(object value) {
        return this.Checker.IsGreaterThanOrEqualTo(this, value);
    }

    public bool IsLessThanOrEqualTo(object value) {
        return this.Checker.IsLessThanOrEqualTo(this, value);
    }

    public override string ToString() {
        return $"{this.Name}: {this.Value}";
    }
}
