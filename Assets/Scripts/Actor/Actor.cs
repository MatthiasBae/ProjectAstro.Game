using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : ActorBase {
   
    public Animator Animator;
    public Rigidbody2D Rigidbody;
    public InputControllerBase InputController;
    public ActorMovementBase Movement;

    public ActorInventories Inventories;

    private void Start() {
        this.InputController = new InputControllerPlayer(this);
        this.Movement = new ActorMovementPlayer(this);
    }
    private void Update() {
        this.Movement.Update();
    }
}
