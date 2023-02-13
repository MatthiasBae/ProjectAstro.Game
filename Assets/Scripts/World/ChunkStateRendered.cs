using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkStateRendered : ChunkStateBase {
	public ChunkStateRendered(Chunk chunk) : base(chunk) {
		this.State = States.Rendered;
		this.LineColor = Color.green;
		this.IsRemovable = false;
	}
	
	public override void Start() {
		base.Start();
		
		this.Chunk.Renderer.RenderChunk();
		this.IsReady = true;
	}
}
