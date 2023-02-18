using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkEvolutionRendered : ChunkEvolutionBase {
	public ChunkEvolutionRendered(ChunkBase chunk) : base(chunk) {
		this.State = States.Render;
		this.Chunk.LineColor = Color.green;
	}
	
	public override void Build() {
		base.Build();
		this.RenderChunk();
	}

	public override void Dismantle() {
		base.Dismantle();
		this.UnrenderChunk();
	}
	
	private void RenderChunk(){
		this.RenderChunkTerrain();
	}
	
	private void RenderChunkTerrain(){
		this.Chunk.Renderer.RenderTerrain();
	}
	
	private void UnrenderChunk(){
		this.UnrenderChunkTerrain();
	}
	
	private void UnrenderChunkTerrain(){
		this.Chunk.Renderer.UnrenderTerrain();
	}
}
