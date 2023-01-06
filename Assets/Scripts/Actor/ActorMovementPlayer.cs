using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMovementPlayer : ActorMovementBase {
    
    public ActorMovementPlayer(Actor actor) : base(actor) {
        this.InputController = new InputControllerPlayer(actor);
    }
}
