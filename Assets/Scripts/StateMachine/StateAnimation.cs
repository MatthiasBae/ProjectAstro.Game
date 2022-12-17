using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAnimation {
    public string Name;
    public Dictionary<string, StateAnimationLayer> Layers;

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

    public void AddLayer(string layer, StateAnimationLayer animationLayer) {
        if(this.Layers.ContainsKey(layer)) {
            return;
        }
        this.Layers.Add(layer, animationLayer);
    }
}
