using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : ActorBase {

    public ActorProfile Profile;
    public ActorBody Body;
    public ActorMetabolism Metabolism;
    public ActorMovementBase Movement;
    
    public ItemDetector ItemDetector;
    public ActorInventories Inventories;

    public Animator Animator;
    public Rigidbody2D Rigidbody;

    private void Start() {
        this.Movement = new ActorMovementPlayer(this);
        this.Metabolism.Initialize(this);
        
    }
    private void Update() {
        this.Movement.Update();
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            var meal = ActorMetabolismStomachContent.Create(TimeManager.Now, 52, 25, 22);
            this.Metabolism.Stomach.AddMeal(meal);
        }
    }
}
