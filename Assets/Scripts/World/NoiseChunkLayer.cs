using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseChunkLayer {
	public Dictionary<Vector2, Noise> NoiseMaps = new Dictionary<Vector2, Noise>();
	
	public Noise GetNoiseChunk(Vector2 sector) {
		if(this.NoiseMaps.ContainsKey(sector)) {
			return this.NoiseMaps[sector];
		}
		return null;
	}
	
	public void AddNoiseChunk(Vector2 sector, Noise noise) {
		if(this.NoiseMaps.ContainsKey(sector)) {
			return;
		}
		this.NoiseMaps.Add(sector, noise);
	}
	
	public void RemoveNoiseChunk(Vector2 sector) {
		if(this.NoiseMaps.ContainsKey(sector)) {
			this.NoiseMaps.Remove(sector);
		}
	}
	
	public bool HasNoiseChunk(Vector2 sector) {
		return this.NoiseMaps.ContainsKey(sector);
	}
	
	public void UpdateNoiseChunk(Vector2 sector, Noise noise) {
		this.NoiseMaps[sector] = noise;
	}
}
