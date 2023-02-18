using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkEvolutionEmpty : ChunkEvolutionBase {
    // Start is called before the first frame update

    public ChunkEvolutionEmpty(ChunkBase chunk) : base(chunk){
        this.State = States.Empty;
        this.Chunk.LineColor = Color.white;
    }

    public override void Build(){
        base.Dismantle();
    }

    public override void Update(){
        base.Update();
        if (this.Chunk.TimerFinished){
            this.Chunk.IsRemovable = true;
        }
    }
}
