using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Animation {
    public string Name;
    public Dictionary<string, AnimationLayer> Layers;
    public Dictionary<string, AnimationParameter> Parameters;
    
    public void SetFrame(string layer, int frame) {
        if(!this.Layers.ContainsKey(layer)) {
            return;
        }
        this.Layers[layer].SetFrame(this.Name, frame);
    }
    public void SetSortingOrder(string layer, int order) {
        if(!this.Layers.ContainsKey(layer)) {
            return;
        }
        this.Layers[layer].SetSortingOrder(order);
    }

    public void AddLayer(string layer, AnimationLayer animationLayer) {
        if(this.Layers.ContainsKey(layer)) {
            return;
        }
        this.Layers.Add(layer, animationLayer);
    }
    public void AddParameter(string name, AnimationParameter parameter) {
        if(this.Parameters.ContainsKey(name)) {
            return;
        }
        this.Parameters.Add(name, parameter);
    }
    public T GetParameterValue<T>(string parameterName) {
        var parameter = this.Parameters[parameterName];
        if(parameter == null) {
            throw new ArgumentException($"Parameter '{parameterName}' not found.");
        }
        return (T)parameter.Value;
    }
    public void SetParameterValue(string parameterName, object value) {
        var parameter = this.Parameters[parameterName];
        if(parameter == null) {
            throw new ArgumentException($"Parameter '{parameterName}' not found.");
        }
        parameter.Value = value;
    }
}
