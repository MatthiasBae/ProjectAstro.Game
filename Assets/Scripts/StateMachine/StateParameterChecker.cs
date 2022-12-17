using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateParameterChecker {

    public enum CompareTypes {
        Equal,
        NotEqual,
        Greater,
        Less,
        GreaterOrEqual,
        LessOrEqual
    }

    public bool Compare(StateParameter parameter, StateParameterCriteria criteria) {
        switch(criteria.Type) {
            case CompareTypes.Equal:
                return this.IsEqual(parameter, criteria.Value);
            case CompareTypes.NotEqual:
                return this.IsNotEqual(parameter, criteria.Value);
            case CompareTypes.Greater:
                return this.IsGreaterThan(parameter, criteria.Value);
            case CompareTypes.Less:
                return this.IsLessThan(parameter, criteria.Value);
            case CompareTypes.GreaterOrEqual:
                return this.IsGreaterThanOrEqualTo(parameter, criteria.Value);
            case CompareTypes.LessOrEqual:
                return this.IsLessThanOrEqualTo(parameter, criteria.Value);
            default:
                throw new Exception("Parameter type is not valid");
        }
    }

    public bool IsEqual(StateParameter parameter, object value) {
        return parameter.Value.Equals(value);
    }

    public bool IsNotEqual(StateParameter parameter, object value) {
        return !parameter.Value.Equals(value);
    }
    public bool IsGreaterThan(StateParameter parameter, object value) {
        if(parameter.Value is IComparable comparable) {
            return comparable.CompareTo(value) > 0;
        }

        throw new InvalidOperationException("Cannot compare values of type " + parameter.Value.GetType().Name);
    }

    public bool IsLessThan(StateParameter parameter, object value) {
        if(parameter.Value is IComparable comparable) {
            return comparable.CompareTo(value) < 0;
        }

        throw new InvalidOperationException("Cannot compare values of type " + parameter.Value.GetType().Name);
    }

    public bool IsGreaterThanOrEqualTo(StateParameter parameter, object value) {
        if(parameter.Value is IComparable comparable) {
            return comparable.CompareTo(value) >= 0;
        }

        throw new InvalidOperationException("Cannot compare values of type " + parameter.Value.GetType().Name);
    }

    public bool IsLessThanOrEqualTo(StateParameter parameter, object value) {
        if(parameter.Value is IComparable comparable) {
            return comparable.CompareTo(value) <= 0;
        }

        throw new InvalidOperationException("Cannot compare values of type " + parameter.Value.GetType().Name);
    }
}
