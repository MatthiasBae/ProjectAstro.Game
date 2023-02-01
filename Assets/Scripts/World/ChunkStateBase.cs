using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChunkStateBase {
	public Chunk Chunk;
	public enum States {Empty, Created, Prepared, Rendered};
	
	public States State = States.Empty;
	public bool IsReady;
	public bool IsRemovable;
	
	public Color LineColor;
	
	public ChunkStateBase(Chunk chunk){
		this.Chunk = chunk;
	}
	
	public virtual void Start(){
		
	}

	public virtual void Update(){
		
	}
	
	public virtual void Exit(){
	}
}
