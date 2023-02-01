using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkStatePrepared : ChunkStateBase
{
    public ChunkStatePrepared(Chunk chunk) : base(chunk)
    {
        this.State = States.Prepared;
        this.LineColor = Color.yellow;
        this.IsRemovable = false;
    }

    public override void Start(){
        base.Start();
        
        this.Chunk.Renderer.LoadChunkData();
        this.IsReady = true;
    }
}

