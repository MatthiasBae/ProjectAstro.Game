using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerPlayer : InputControllerBase {
    public KeyBindings KeyBindings = new KeyBindings();

    public override Vector2 MoveDirection {
        get => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    public override Vector2 LookDirection {
        get => (Vector2)((Vector3)this.MouseWorldPosition - this.Actor.transform.position).normalized;
    }
    public override Vector2 MouseWorldPosition {
        get => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public override Vector2 Target {
        get => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //oder: get => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public override bool Walk {
        get => (Input.GetKey(this.KeyBindings.WalkUp)
            || Input.GetKey(this.KeyBindings.WalkDown)
            || Input.GetKey(this.KeyBindings.WalkRight)
            || Input.GetKey(this.KeyBindings.WalkLeft));
    }
    public override bool Run {
        get => this.Walk
            && Input.GetKey(KeyCode.LeftShift);
    }
    public override bool Aim {
        get => Input.GetKey(this.KeyBindings.Aim) && (!this.Walk && !this.Run);
    }

    public InputControllerPlayer(Actor actor) : base(actor) {
    }

}
