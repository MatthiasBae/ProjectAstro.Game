using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkEvolutionCreated : ChunkEvolutionBase {
	public ChunkEvolutionCreated(ChunkBase chunk) : base(chunk) {
		this.State = States.Create;
		this.Chunk.LineColor = Color.red;
	}
	
	public override void Build() {
		base.Build();
		this.CreateFields();
	}
	
	public override void Dismantle() {
		base.Dismantle();
		this.DestroyFields();
	}

	private void CreateFields(){
		this.Chunk.Fields = new List<ChunkField>();
		for(var y = 0; y < this.Chunk.Size.y; y++){
			for(var x = 0; x < this.Chunk.Size.x; x++){
				var positionInChunk =  new Vector2Int(x, y);
				var worldPosition = ChunkHelper.PositionInChunkToWorldPosition(this.Chunk.Sector, positionInChunk, this.Chunk.Size);
				var field = ChunkField.Create(worldPosition, positionInChunk);
				this.Chunk.Fields.Add(field);
			}
		}
	}
	
	private void DestroyFields(){
		this.Chunk.Fields.Clear();
	}
}
