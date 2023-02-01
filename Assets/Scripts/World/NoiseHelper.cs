using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoiseHelper {
	public static Vector2 ChunkLocalSectorToNoiseLocalSector (Vector2 localSector, Vector2 noiseSize) {
		var noiseSector = new Vector2{
			x = localSector.x + Mathf.Ceil((noiseSize.x - 1)/2) ,
			y = -localSector.y + Mathf.Floor((noiseSize.y - 1)/2) 
		};
		return noiseSector;
	}
}
