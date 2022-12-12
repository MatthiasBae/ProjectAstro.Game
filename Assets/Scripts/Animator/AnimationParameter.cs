using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationParameter {
    public string Name;
    public object Value;

    public void Set(string name, object value) {
        this.Name = name;
        this.Value = value;
    }
}
