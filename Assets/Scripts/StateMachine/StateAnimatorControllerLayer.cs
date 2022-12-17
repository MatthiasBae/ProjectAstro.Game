using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAnimatorControllerLayer {
    public Dictionary<string, Animation> Animations;
    public Animation ActiveAnimation;

    public void AddAnimation(string name, Animation animation) {
        if(this.Animations.ContainsKey(name)) {
            return;
        }
        this.Animations.Add(name, animation);
    }

    public Animation GetAnimation(string name) {
        if(!this.Animations.ContainsKey(name)) {
            return null;
        }
        return this.Animations[name];
    }

    public void Play(string name) {
        Animation animation = this.GetAnimation(name);
        if(animation == null) {
            return;
        }
    }
}
