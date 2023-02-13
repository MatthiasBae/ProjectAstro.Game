using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour  {
    public static WorldGenerator Instance;

    public MapGenerator MapGenerator;
    public ChunkManager ChunkManager;

    public void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    public void Start() {
        this.MapGenerator.Start();
        
        this.RegisterEvents();
        this.ChunkManager.CreateChunks();
    }

    public void RegisterEvents() {
        //this.ChunkManager.ChunkAdded += (chunk) => this.MapGenerator.CreateNoises();
        this.ChunkManager.ChunkAdded += (chunk) => this.MapGenerator.CreateNoise(chunk);
        this.ChunkManager.ChunkRemoved += (chunk) => this.MapGenerator.RemoveNoise(chunk);
    }
    private void Update() {
        this.ChunkManager.UpdateActorTrackers();
        this.ChunkManager.CheckChunkCreation();
        
        this.ChunkManager.UpdateChunks();
        
        
    }
}
