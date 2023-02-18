using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public event Action<ChunkBase> ChunkAdded;
    public event Action<ChunkBase> ChunkRemoved;

    private void Awake() {
        Instance = this;
        Instance.Initialize();
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
        var chunksToAdd = new Dictionary<Vector2, ChunkBase>();
        var playerSector = this.ActorTracker.Sector;
        
        for(int y = -(int)this.ChunkCount.y / 2; y < (int)this.ChunkCount.y / 2; y++) {
            for(int x = -(int)this.ChunkCount.x / 2; x < (int)this.ChunkCount.x / 2; x++) {
                var chunkPosition = new Vector2(x, y);
                var chunkSector = new Vector2(playerSector.x + chunkPosition.x, playerSector.y + chunkPosition.y);

                if(this.Store.Chunks.ContainsKey(chunkSector)) {
                    continue;
                }
                //Debug.Log("Creating chunk " + chunkSector + "");
                var chunk = ChunkBase.Create(this.ActorTracker, this.ChunkSize, chunkSector, this.ChunkDuration, ChunkEvolutionBase.States.Empty);
                
                chunksToAdd.Add(chunkSector, chunk);
                
            }
        }

        foreach(var chunk in chunksToAdd) {
            this.Store.Add(chunk.Key, chunk.Value);
            this.ChunkAdded?.Invoke(chunk.Value);
            chunk.Value.Evolution.Start();
        }
    }

    public void UpdateChunks(){
        foreach (var chunk in this.Store.Chunks.Values) {
            this.CheckSwitchChunkState(chunk);
            chunk.Update();
            
            //Debug.Log($"Chunk {chunk.Sector} is in state {chunk.Evolution.State}");
            chunk.RenderChunkInEditor(chunk.LineColor);
            
        }
    }
        
    public void CheckSwitchChunkState(ChunkBase chunk) {
        var isInRange = chunk.IsInRange;
        var isReady = chunk.Evolution.IsReady;
        var chunkState = chunk.Evolution.State;
        var timerFinished = chunk.TimerFinished;
        
        var chunksToRender = ChunkManager.Instance.ChunkRowsRenderState;
        var chunksToPrepare = ChunkManager.Instance.ChunkRowsPrepareState;
        var chunksToCreate = ChunkManager.Instance.ChunkRowsCreateState;
        var isInRenderDistance = ChunkHelper.ChunkIsInRange(chunk.Sector, chunk.ActorTracker.Sector, chunksToRender);
        var isInPrepareDistance = ChunkHelper.ChunkIsInRange(chunk.Sector, chunk.ActorTracker.Sector, chunksToPrepare);
        var isInCreateDistance = ChunkHelper.ChunkIsInRange(chunk.Sector, chunk.ActorTracker.Sector, chunksToCreate);
        
        if (!isReady){
            Debug.Log("Chunk is not ready");
            return;
        }
        
        if (isInRange){
            chunk.ResetTimer();
        }
        
        if(chunkState == ChunkEvolutionBase.States.Empty && isInCreateDistance){
            var evolution = new ChunkEvolutionCreated(chunk);
            chunk.Evolution.SwitchEvolutionState(chunk, evolution);
        }
        
        if(chunkState == ChunkEvolutionBase.States.Create && isInPrepareDistance){
            var evolution = new ChunkEvolutionPrepared(chunk);
            chunk.Evolution.SwitchEvolutionState(chunk, evolution);
        }
        
        if (chunkState == ChunkEvolutionBase.States.Prepare && isInRenderDistance){
            var evolution = new ChunkEvolutionRendered(chunk);
            chunk.Evolution.SwitchEvolutionState(chunk, evolution);
        }
        
        if (chunkState == ChunkEvolutionBase.States.Render && !isInRenderDistance && timerFinished){
            var evolution = new ChunkEvolutionPrepared(chunk);
            chunk.Evolution.SwitchEvolutionState(chunk, evolution, true);
        }
        
        if (chunkState == ChunkEvolutionBase.States.Prepare && !isInPrepareDistance && timerFinished){
            var evolution = new ChunkEvolutionCreated(chunk);
            chunk.Evolution.SwitchEvolutionState(chunk, evolution, true);
        }
        
        if (chunkState == ChunkEvolutionBase.States.Create && !isInCreateDistance && timerFinished){
            var evolution = new ChunkEvolutionEmpty(chunk);
            chunk.Evolution.SwitchEvolutionState(chunk, evolution, true);
        }
    }
 
    public void UpdateActorTrackers() {
        this.ActorTracker.UpdatePlayerSector();
        if(this.ActorTracker.SwitchedChunk) {
            //Debug.Log("Player is in chunk " + this.ActorTracker.Sector);
            this.PlayerSwitchedChunk?.Invoke();
        }
    }

    public void RemoveChunks() {
        var chunks = this.Store.Chunks.Values.ToArray();
        foreach(var chunk in chunks) {
            if(chunk.IsRemovable) {
                this.Store.Chunks.Remove(chunk.Sector);
            }
        }
    }
}
