using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRenderer {
	private readonly Chunk Chunk;
	private readonly MapGenerator MapGenerator;

	public ChunkRenderer(Chunk chunk){
		this.Chunk = chunk;
		this.MapGenerator = WorldGenerator.Instance.MapGenerator;
	}
	
	//@TODO: ChunkLoader Klasse erstellen das hier auslagern
	public void LoadChunkData(){
		this.MapGenerator.GetChunkTerrainData(this.Chunk);
	}
	
	public void RenderChunk(){
		foreach (var field in this.Chunk.Fields){
			var terrainLayer = this.MapGenerator.GetTerrainLayer("Ground");
			//Debug.Log(field.FieldController.FieldTerrain.BiomeConfig.RuleTile + " an Position " + field.WorldPosition);
			//Debug.Log(($"TerrainLayer: {terrainLayer} Tilemap: {terrainLayer.Tilemap} WorldPosition: {field.WorldPosition} Chunk.Sector: {this.Chunk.Sector}"));
			TerrainEditor.SetTile(terrainLayer.Tilemap, field.WorldPosition, field.FieldController.FieldTerrain.BiomeConfig.RuleTile);
		}
	}
}
