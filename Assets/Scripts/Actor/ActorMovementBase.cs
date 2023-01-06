using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorMovementBase {
    private ActorBase Actor;
    private Rigidbody2D ActorRigidbody;
    private Animator ActorAnimator;
    private ActorConfig ActorConfig;
    public InputControllerBase InputController;

    private Dictionary<int, float> StateSpeeds;
    private float CurrentSpeed;
    
    private Vector2 PreviousMoveDirection;
    private Vector2 PreviousLookDirection;
    private Vector2 PreviousDirection;
    private bool PreviousAim;
    private int PreviousState;

    public ActorMovementBase(Actor actor) {
        this.Actor = actor;
        this.ActorRigidbody = actor.Rigidbody;
        this.ActorAnimator = actor.Animator;
        this.ActorConfig = actor.Config;

        this.PopulateSpeed();
    }

    private void PopulateSpeed() {
        this.StateSpeeds = new Dictionary<int, float>() {
            {0, 0f},
            {1, this.ActorConfig.WalkSpeed},
            {2, this.ActorConfig.RunSpeed}
        };
    }

    public void Update() {
        this.UpdateState();
        this.UpdateAim();
        this.UpdateDirection();
        this.UpdateMovement();
    }

    private void UpdateState() {

        var stateWalk = this.InputController.Walk;
        var stateRun = this.InputController.Run;

        var movement = 0;
        if(stateWalk) {
            movement = 1;
        }
        if(stateRun) {
            movement = 2;
        }
        
        this.SetAnimatorParameterState(movement);
        this.CurrentSpeed = this.StateSpeeds[movement];
        this.PreviousState = movement;

    }


    private void UpdateAim() {
        var aim = this.InputController.Aim;

        if(aim != this.PreviousAim) {
            this.SetAnimatorParameterAim(aim);
            this.PreviousAim = aim;
        }
    }

    private void UpdateDirection() {
        var moveDirection = Vector2Int.RoundToInt(this.InputController.MoveDirection);
        var lookDirection = Vector2Int.RoundToInt(this.InputController.LookDirection);

        if(moveDirection != Vector2.zero) {
            this.SetAnimatorParameterDirection(moveDirection);
            this.PreviousMoveDirection = moveDirection;
            return;
        }

        if(lookDirection == this.PreviousLookDirection) {
            return;
        }
        this.SetAnimatorParameterDirection(lookDirection);
        this.PreviousLookDirection = lookDirection;
    }

    public void UpdateMovement() {
        var movement = this.InputController.MoveDirection;
        var speed = this.CurrentSpeed;
        this.Move(movement, speed);
    }

    private void SetAnimatorParameterHandsEquipped(int handsEquipped) {
        this.ActorAnimator.SetInteger("HandsEquipped", handsEquipped);
    }

    private void SetAnimatorParameterState(int state) {
        this.ActorAnimator.SetInteger("State", state);
    }

    private void SetAnimatorParameterAim(bool aim) {
        this.ActorAnimator.SetBool("Aim", aim);
    }

    private void SetAnimatorParameterDirection(Vector2 direction) {
        this.ActorAnimator.SetFloat("XDirection", Mathf.Round(direction.x));
        this.ActorAnimator.SetFloat("YDirection", Mathf.Round(direction.y));
    }

    public virtual void Move(Vector2 direction, float speed) {
        if(direction == Vector2.zero) {
            return;
        }
        this.Actor.transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
    public virtual void AddForce(Vector2 direction, float strength) {
        this.ActorRigidbody.AddForce(direction * strength, ForceMode2D.Impulse);
    }
}
