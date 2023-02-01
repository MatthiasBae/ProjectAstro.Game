using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController {
	public FieldTerrain FieldTerrain;
	public Vector2Int Position;
	public FieldController(FieldTerrain fieldTerrain){
		this.FieldTerrain = fieldTerrain;
	}
}
