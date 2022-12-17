using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : ActorBase {
   
    public Animator Animator;
    public Rigidbody2D Rigidbody;
    public InputControllerBase InputController;
    public ActorMovementBase Movement;

    public StateMachine StateMachine;
    public StateParamaterLibraryBase StateParamaterLibrary;
    public StateLibraryBase StateLibrary;
    
    

    public ActorInventories Inventories;

    private void Start() {
        this.InputController = new InputControllerPlayer(this);
        this.Movement = new ActorMovementPlayer(this);
        this.StateMachine = new StateMachine();
        this.StateParamaterLibrary = new StateParamaterLibraryHuman();
        this.StateMachine.Parameters = this.StateParamaterLibrary.Parameters;
        this.StateLibrary = new StateLibraryHuman(this.StateMachine);
        this.StateMachine.SetStateLibrary(this.StateLibrary);
    }
    private void Update() {
        this.StateMachine.Update();
        this.Movement.Update();
    }
}
