using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkEvolutionPrepared : ChunkEvolutionBase {

	public ChunkEvolutionPrepared(ChunkBase chunk) : base(chunk)  {
		this.State = States.Prepare;
		this.Chunk.LineColor = Color.yellow;
	}
	
	public override void Build() {
		base.Build();
		this.LoadChunkData();
	}
	
	public override void Dismantle() {
		base.Dismantle();
		this.UnloadTerrainData();
	}
	
	private void LoadChunkData(){
		this.LoadChunkTerrainData();
	}

	private void LoadChunkTerrainData(){
		var mapGenerator = MapGenerator.Instance;
		
		foreach (var chunkField in this.Chunk.Fields){
			var fieldTerrain = mapGenerator.GetFieldTerrain(this.Chunk.Sector, chunkField.PositionInChunk);
			chunkField.FieldController = new FieldController(fieldTerrain);
		}
	}
	
	private void UnloadTerrainData(){
		foreach(var chunkField in this.Chunk.Fields){
			chunkField.FieldController = null;
		}
	}
}
