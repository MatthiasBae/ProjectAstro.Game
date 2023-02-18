using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRenderer {
	//private readonly Chunk Chunk;
	private readonly ChunkBase Chunk; 
	private readonly MapGenerator MapGenerator;

	// public ChunkRenderer(Chunk chunk){
	// 	this.Chunk = chunk;
	// 	this.MapGenerator = WorldGenerator.Instance.MapGenerator;
	// }
	
	public ChunkRenderer(ChunkBase chunk){
		this.Chunk = chunk;
		this.MapGenerator = WorldGenerator.Instance.MapGenerator;
	}
	
	//@TODO: ChunkLoader Klasse erstellen das hier auslagern
	// public void LoadChunkData(){
	// 	this.MapGenerator.GetChunkTerrainData(this.Chunk);
	// }
	
	//@TODO: RenderTerrain layer als Parameter übergeben können
	public void RenderTerrain(){
		foreach (var field in this.Chunk.Fields){
			var terrainLayer = this.MapGenerator.GetTerrainLayer("Ground");
			TerrainEditor.SetTile(terrainLayer.Tilemap, field.WorldPosition, field.FieldController.FieldTerrain.BiomeConfig.RuleTile);
		}
	}
	
	public void UnrenderTerrain(){
		foreach (var field in this.Chunk.Fields){
			var terrainLayer = this.MapGenerator.GetTerrainLayer("Ground");
			TerrainEditor.RemoveTile(terrainLayer.Tilemap, field.WorldPosition);
		}
	}
}
