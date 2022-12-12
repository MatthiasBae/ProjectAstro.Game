using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class InputControllerBase {

    public Actor Actor;

    public virtual bool Walk { get; set; }
    public virtual bool Run { get; set; }
    public virtual bool Aim { get; set; }
    public virtual Vector2 MoveDirection { get; set;}
    public virtual Vector2 LookDirection { get; set; }
    public virtual Vector2 MouseWorldPosition { get; set;}
    public virtual Vector2 Target { get; set; }

    public InputControllerBase(Actor actor) {
        this.Actor = actor;
    }
}
