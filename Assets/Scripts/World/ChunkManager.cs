using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ChunkManager : MonoBehaviour {

    public static ChunkManager Instance;
    public Vector2 ChunkSize;
    public Vector2 ChunkCount;

    public Vector2 MapSize{
        get => this.ChunkSize * this.ChunkCount;
    }
    
    public int ChunkRowsRenderState;
    public int ChunkRowsPrepareState;
    public int ChunkRowsCreateState;
    
    public float ChunkDuration;
    public Vector2 Offset {
        get => this.ChunkSize * this.ActorTracker.Sector;
    }

    public ChunkActorTracker ActorTracker;
    public ChunkStore Store;
    
    public event Action PlayerSwitchedChunk;
    public event Action<Chunk> ChunkAdded;
    public event Action<Chunk> ChunkRemoved;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            Instance.Initialize();
        }
        else {
            Destroy(this);
        }
    }

    public void Initialize() {
        this.Store = new ChunkStore();
    }

    public void CheckChunkCreation() {
        this.RemoveChunks();
        if (!this.ActorTracker.SwitchedChunk){
            return;
        }
        this.CreateChunks();
    }
    public void CreateChunks() {
        var chunksToAdd = new Dictionary<Vector2, Chunk>();
        var playerSector = this.ActorTracker.Sector;
        
        for(int y = -(int)this.ChunkCount.y / 2; y < (int)this.ChunkCount.y / 2; y++) {
            for(int x = -(int)this.ChunkCount.x / 2; x < (int)this.ChunkCount.x / 2; x++) {
                var chunkPosition = new Vector2(x, y);
                var chunkSector = new Vector2(playerSector.x + chunkPosition.x, playerSector.y + chunkPosition.y);

                if(this.Store.Chunks.ContainsKey(chunkSector)) {
                    continue;
                }
                //Debug.Log("Creating chunk " + chunkSector + "");
                var chunk = Chunk.Create(this.ChunkSize, chunkSector, this.ChunkDuration);
                chunksToAdd.Add(chunkSector, chunk);
                
            }
        }

        foreach(var chunk in chunksToAdd) {
            this.Store.Add(chunk.Key, chunk.Value);
            this.ChunkAdded?.Invoke(chunk.Value);
        }
    }

    public void UpdateChunks(){
        foreach (var chunk in this.Store.Chunks.Values) {
            chunk.Update();
            //Debug.Log($"Chunk {chunk.Sector} is in state {chunk.State}");
            chunk.RenderChunkInEditor(chunk.State.LineColor);
            
        }
    }
    
    public void UpdateActorTrackers() {
        this.ActorTracker.UpdatePlayerSector();
        if(this.ActorTracker.SwitchedChunk) {
            //Debug.Log("Player is in chunk " + this.ActorTracker.Sector);
            this.PlayerSwitchedChunk?.Invoke();
        }
    }

    public bool TryRemoveChunk(Chunk chunk) {
        if(this.Store.Chunks.ContainsKey(chunk.Sector)) {
            this.Store.Remove(chunk.Sector);
            return true;
        }
        return false;
    }
    
    public void RemoveChunks() {
        var chunksToRemove = new List<Chunk>();
        foreach(var chunk in this.Store.Chunks.Values) {
            var isInRange = chunk.IsInRange(this.ActorTracker);
            
            
            if(isInRange) {
                chunk.ResetTimer();
            }
            else {
                chunk.UpdateTimer();
                if(chunk.TimerFinished && chunk.State.IsRemovable) {
                    Debug.Log("Timer finished");
                    chunksToRemove.Add(chunk);
                    this.ChunkRemoved?.Invoke(chunk);
                }
            }
        }

        foreach(var chunk in chunksToRemove) {
            this.Store.Remove(chunk.Sector);
        }
    }
    public void RegisterActorTracker(ChunkActorTracker tracker) {
        this.ActorTracker = tracker;
    }
}
