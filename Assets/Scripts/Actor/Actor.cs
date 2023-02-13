using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Actor : ActorBase {

    public ActorProfile Profile;
    public ActorBody Body;
    public ActorMetabolism Metabolism;
    public ActorMovementBase Movement;
    public List<ActorPart> Parts;

    public InventoryManager InventoryManager;
    public EquipmentManager EquipmentManager;
    
    public Animator Animator;
    public Rigidbody2D Rigidbody;

    private void Awake() {
        this.Movement = new ActorMovementPlayer(this);
        this.Metabolism.Initialize(this);
        

    }

    private void Start() {
        var actorTracker = new ChunkActorTracker();
        actorTracker.RegisterPlayer(this);
        //ChunkManager.Instance.RegisterActorTracker(actorTracker);

    }
    private void Update() {
        this.Movement.Update();
        
    }
}
