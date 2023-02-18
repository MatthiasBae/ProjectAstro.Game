using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainEditor {
	public static void SetTile(Tilemap tilemap, Vector2 position, TileBase tile) {
		var vec3Position = Vector3Int.RoundToInt(position);
		tilemap.SetTile(vec3Position, tile);
	}
	
	public static void RemoveTile(Tilemap tilemap, Vector2 position) {
		var vec3Position = Vector3Int.RoundToInt(position);
		tilemap.SetTile(vec3Position, null);
	}
}
