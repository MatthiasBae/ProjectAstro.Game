using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkStore {
    public Dictionary<Vector2, ChunkBase> Chunks = new Dictionary<Vector2, ChunkBase>();

    public void Add(Vector2 sector, ChunkBase chunk) {
        if(this.Chunks.ContainsKey(sector)) {
            return;
        }

        this.Chunks.Add(sector, chunk);
    }
    
    public ChunkBase Get(Vector2 sector) {
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
