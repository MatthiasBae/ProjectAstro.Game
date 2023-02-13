using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChunkHelper {

    public static Vector2 WorldToSector(Vector2 position, Vector2 chunkSize) {
        return new Vector2(Mathf.Floor(position.x / chunkSize.x), Mathf.Floor(position.y / chunkSize.y));
    }

    public static Vector2 SectorToWorld(Vector2 sector, Vector2 chunkSize) {
        return new Vector2(sector.x * chunkSize.x, sector.y * chunkSize.y);
    }

    public static Vector2 WorldToChunkField(Vector2 position, Vector2 chunkSize) {
        return new Vector2{
            x = Mathf.Floor(Mathf.Abs(position.x) % chunkSize.x),
            y = Mathf.Floor(Mathf.Abs(position.y) % chunkSize.y)
        };
    }

    public static Vector2 PositionInChunkToWorldPosition(Vector2 sector, Vector2 positionInChunk, Vector2 chunkSize){
        return new Vector2{
            x = sector.x * chunkSize.x + positionInChunk.x,
            y = sector.y * chunkSize.y + positionInChunk.y
        };

    }
    
    public static bool ChunkIsInRange(Vector2 sector, Vector2 midSector, int range) {
        var rangeX = range;
        var rangeY = range;

        if (sector.x > 0){
            rangeX -= 1;
        }
        if (sector.y > 0){
            rangeY -= 1;
        }

        var isSectorInRange = Mathf.Abs(sector.x - midSector.x) <= rangeX && Mathf.Abs(sector.y - midSector.y) <= rangeY;
        return isSectorInRange;
    }
}
