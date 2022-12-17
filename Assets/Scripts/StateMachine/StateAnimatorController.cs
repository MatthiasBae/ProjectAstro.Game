using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAnimatorController {
    private Dictionary<string, StateAnimatorControllerLayer> Layers;

    public void AddLayer(string name, StateAnimatorControllerLayer layer) {
        if(this.Layers.ContainsKey(name)) {
            return;
        }
        this.Layers.Add(name, layer);
    }

    public StateAnimatorControllerLayer GetLayer(string name) {
        if(!this.Layers.ContainsKey(name)) {
            return null;
        }
        return this.Layers[name];
    }

    public void RemoveLayer(string name) {
        if(!this.Layers.ContainsKey(name)) {
            return;
        }
        this.Layers.Remove(name);
    }

    public void Play(string layer, string animationName) {
        var animatorControllerLayer = this.GetLayer(layer);
        if(animatorControllerLayer == null) {
            return;
        }
        animatorControllerLayer.Play(animationName);
    }
}
