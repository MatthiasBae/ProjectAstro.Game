using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkStore {
    public Dictionary<Vector2, Chunk> Chunks = new Dictionary<Vector2, Chunk>();

    public void Add(Vector2 sector, Chunk chunk) {
        if(this.Chunks.ContainsKey(sector)) {
            return;
        }

        this.Chunks.Add(sector, chunk);
    }
    
    public Chunk Get(Vector2 sector) {
        if(this.Chunks.ContainsKey(sector)) {
            return this.Chunks[sector];
        }

        return null;
    }
    
    public void Remove(Vector2 sector) {
        if(this.Chunks.ContainsKey(sector)) {
            this.Chunks.Remove(sector);
        }
    }
}
