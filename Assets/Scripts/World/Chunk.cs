using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Chunk {
    public Vector2 Size;
    public Vector2 Sector;
    public Vector2 LocalSector{
        get => this.Sector - ChunkManager.Instance.ActorTracker.Sector;
    }

    public Noise Noise;
    
    private float Duration;
    private float DurationTimer;
    public bool TimerFinished;
    
    //@TODO: Integrate states into chunk as abstract class
    public ChunkStateBase State;
    public ChunkRenderer Renderer;
    
    public List<ChunkField> Fields;
    
    public static Chunk Create(Vector2 size, Vector2 sector, float duration) {
        var chunk = new Chunk() {
            Size = size,
            Sector = sector,
            Duration = duration,
            Fields = new List<ChunkField>(),
        };
        chunk.State = new ChunkStateEmpty(chunk);
        chunk.Renderer = new ChunkRenderer(chunk);
        return chunk;
    }

    public void CheckSwitchState(){
        var chunksToRender = ChunkManager.Instance.ChunkRowsRenderState;
        var chunksToPrepare = ChunkManager.Instance.ChunkRowsPrepareState;
        var chunksToCreate = ChunkManager.Instance.ChunkRowsCreateState ;
        
        var localSector = this.Sector - ChunkManager.Instance.ActorTracker.Sector;
        
        var isInRenderRange = ChunkHelper.ChunkIsInRange(localSector, Vector2.zero, chunksToRender);
        var isInPrepareRange = ChunkHelper.ChunkIsInRange(localSector, Vector2.zero, chunksToPrepare);
        var isInCreateRange = ChunkHelper.ChunkIsInRange(localSector, Vector2.zero, chunksToCreate);
        
        if(isInRenderRange
           && (this.State.State == ChunkStateBase.States.Prepared)
           && this.State.IsReady){
            Debug.Log("Switch to Rendered from Prepared");
            this.SwitchState(new ChunkStateRendered(this));
            return;
        }
        
        if((!isInRenderRange && this.State.State == ChunkStateBase.States.Rendered) 
           || (isInPrepareRange && this.State.State == ChunkStateBase.States.Created)){
            //Debug.Log("Switch to Prepared from " + this.State.State.ToString());
            this.SwitchState(new ChunkStatePrepared(this));
            return;
        }

        if((!isInPrepareRange && this.State.State == ChunkStateBase.States.Prepared) 
           || (isInCreateRange && this.State.State == ChunkStateBase.States.Empty)){
            //Debug.Log("Switch to Created from " + this.State.State.ToString());
            this.SwitchState(new ChunkStateCreated(this));
            return;
        }
        
        if(!isInCreateRange && this.State.State == ChunkStateBase.States.Created){
            //Debug.Log("Switch to Empty from Created");
            this.SwitchState(new ChunkStateEmpty(this));
            return;
        }
    }
    
    public void Update(){
        this.CheckSwitchState();
        this.State.Update();
        
    }
    
    private void SwitchState(ChunkStateBase stateBase){
        this.State?.Exit();
        this.State = stateBase;
        this.State.Start();
    }
        
    public void CreateFields(){
        for(var y = 0; y < this.Size.y; y++){
            for(var x = 0; x < this.Size.x; x++){
                var positionInChunk =  new Vector2Int(x, y);
                var worldPosition = ChunkHelper.PositionInChunkToWorldPosition(this.Sector, positionInChunk, this.Size);
                
                Debug.Log($"Erstellen: World: {worldPosition} ChunkField: {positionInChunk}");
                var field = ChunkField.Create(worldPosition, positionInChunk);
                this.Fields.Add(field);
            }
        }
    }

    public bool IsInRange(ChunkActorTracker chunkActorTracker) {
        var halfChunkCount = ChunkManager.Instance.ChunkCount / 2;
        // Creates a circle of chunks around the player
        // var chunkDistance = Vector2.Distance(this.Sector, chunkActorTracker.Sector);
        // if(chunkDistance > halfChunkCount.x) {
        //     return false;
        // }
        //
        // return true;
        
        // Creates a square of chunks around the player
        var chunkIsInRange = this.Sector.x >= chunkActorTracker.Sector.x - halfChunkCount.x &&
                              this.Sector.x <= chunkActorTracker.Sector.x + halfChunkCount.x &&
                              this.Sector.y >= chunkActorTracker.Sector.y - halfChunkCount.y &&
                              this.Sector.y <= chunkActorTracker.Sector.y + halfChunkCount.y;
        return chunkIsInRange;

    }

    public void UpdateTimer() {
        if(this.DurationTimer <= this.Duration) {
            this.DurationTimer += Time.deltaTime;
            return;
        }
        this.TimerFinished = true;
    }

    public void ResetTimer() {
        this.DurationTimer = 0;
    }

    public static Chunk Aquire(Vector2 size, Vector2 sector, float duration) {
        var chunk = ObjectPool<Chunk>.Instance.AcquireReusable();

        chunk.Duration = duration;
        chunk.Size = size;
        chunk.Sector = sector;
        return chunk;
    }

    public void RenderChunkInEditor(Color color) {
        var position = ChunkHelper.SectorToWorld(this.Sector, this.Size);
        var size = this.Size;

        Debug.DrawLine(position, position + new Vector2(size.x, 0), color);
        Debug.DrawLine(position, position + new Vector2(0, size.y), color);
        Debug.DrawLine(position + new Vector2(size.x, 0), position + new Vector2(size.x, size.y), color);
        Debug.DrawLine(position + new Vector2(0, size.y), position + new Vector2(size.x, size.y), color);
    }
}
