using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActorBody {

    //@TODO: Actorbodypart module erstellen, die jeweils auch ein eigenes Gewicht haben
    
    public float Weight {
        get => this.Rigidbody.mass;
        set => this.Rigidbody.mass = value;
    }
    public float BMI {
        get => this.Weight / (this.Height * this.Height);
    }
    public int Height;
    public float Fat;
    
    [SerializeField]
    private Rigidbody2D Rigidbody;

}
