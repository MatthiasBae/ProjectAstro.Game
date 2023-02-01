using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChunkStateCreated : ChunkStateBase {
	public ChunkStateCreated(Chunk chunk) : base(chunk) {
		this.State = States.Created;
		this.LineColor = Color.red;
		this.IsRemovable = false;
	}	

	public override void Start() {
		base.Start();
		
		this.Chunk.CreateFields();
		this.IsReady = true;
	}
}
