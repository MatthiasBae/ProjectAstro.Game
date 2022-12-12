using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorBase : MonoBehaviour {
    public ActorConfig Config;
    public static Actor Create() {
        return new Actor();
    }
}
