using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseChunkStore {
    public Dictionary<string, NoiseChunkLayer> NoiseLayers;
    
    public NoiseChunkStore() {
        this.NoiseLayers = new Dictionary<string, NoiseChunkLayer>();
    }
    
    public Noise GetNoiseChunk(string layer, Vector2 sector) {
        if(this.NoiseLayers.ContainsKey(layer)) {
            return this.NoiseLayers[layer].GetNoiseChunk(sector);
        }
        return null;
    }
    
    public void AddNoiseChunk(string layer, Vector2 sector, Noise noise) {
        if(this.NoiseLayers.ContainsKey(layer)) {
            this.NoiseLayers[layer].AddNoiseChunk(sector, noise);
            return;
        }
        this.NoiseLayers.Add(layer, new NoiseChunkLayer());
        this.NoiseLayers[layer].AddNoiseChunk(sector, noise);
    }
    
    public void RemoveNoiseChunk(string layer, Vector2 sector) {
        if(this.NoiseLayers.ContainsKey(layer)) {
            this.NoiseLayers[layer].RemoveNoiseChunk(sector);
        }
    }
    
    public void UpdateNoiseChunk(string layer, Vector2 sector, Noise noise) {
        if(this.NoiseLayers.ContainsKey(layer)) {
            this.NoiseLayers[layer].UpdateNoiseChunk(sector, noise);
        }
    }   
    
    public bool HasNoiseChunk(string layer, Vector2 sector) {
        if(this.NoiseLayers.ContainsKey(layer)) {
            return this.NoiseLayers[layer].HasNoiseChunk(sector);
        }
        return false;
    }
}