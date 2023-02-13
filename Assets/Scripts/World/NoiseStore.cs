using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseStore {
    
    public  Dictionary<string, Noise> NoiseLayer = new Dictionary<string, Noise>();
    
    public void AddNoise(string layer, Noise noise) {
        if(this.HasNoise(layer)) {
            this.UpdateNoise(layer, noise);
            return;
        }
        this.NoiseLayer.Add(layer, noise);
    }
    
    public void UpdateNoise(string layer, Noise noise) {
        this.NoiseLayer[layer] = noise;
    }
    
    public void RemoveNoise(string layer) {
        this.NoiseLayer.Remove(layer);
    }
    
    public Noise GetNoise(string layer) {
        return this.NoiseLayer[layer];
    }

    public Noise GetNoiseChunk(string layer, Vector2 localNoiseSector, Vector2 size){
        if (!this.HasNoise(layer)){
            return null;
        }
        
        Noise noise = this.GetNoise(layer);
        Noise newNoise = new Noise();
        float[,] noiseChunk = new float[(int)size.x, (int)size.y];
        
        for (int x = 0; x < size.x; x++){
            for (int y = 0; y < size.y; y++){
                noiseChunk[x, y] = noise.Map[(int)(localNoiseSector.x * size.x + x), (int)(localNoiseSector.y * size.y + y)];
            }
        }
        newNoise.Config = noise.Config;
        newNoise.Map = noiseChunk;
        return newNoise;
    }
    
    public bool HasNoise(string layer) {
        return this.NoiseLayer.ContainsKey(layer);
    }
}
