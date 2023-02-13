using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkField {
    public Vector2 WorldPosition;
    public Vector2Int PositionInChunk;
    
    public FieldController FieldController;
    
    public static ChunkField Create(Vector2 worldPosition, Vector2Int positionInChunk) {
        var chunkField = new ChunkField() {
            WorldPosition = worldPosition,
            PositionInChunk = positionInChunk,
        };
        return chunkField;
    }

    public void Render(){
        //Welches BIOM ist an der Koordinate?
        
    }
}
