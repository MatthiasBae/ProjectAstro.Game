using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : ActorBase {
   
    public Animator Animator;
    public Rigidbody2D Rigidbody;
    
    
    public ActorMovementBase Movement;
    public ItemDetector ItemDetector;
    public ActorInventories Inventories;

    private void Start() {
        this.Movement = new ActorMovementPlayer(this);
    }
    private void Update() {
        this.Movement.Update();
    }
}
