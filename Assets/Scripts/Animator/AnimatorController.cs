using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController {
    private Dictionary<string, AnimatorControllerLayer> Layers;

    public void AddLayer(string name, AnimatorControllerLayer layer) {
        if(this.Layers.ContainsKey(name)) {
            return;
        }
        this.Layers.Add(name, layer);
    }

    public AnimatorControllerLayer GetLayer(string name) {
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

    public void Play(string layer, string animation) {
        AnimatorControllerLayer animatorControllerLayer = this.GetLayer(layer);
        if(animatorControllerLayer == null) {
            return;
        }
        animatorControllerLayer.Play(animation);
    }
}
