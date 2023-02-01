using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkStateEmpty : ChunkStateBase {
	public ChunkStateEmpty(Chunk chunk) : base(chunk){
		this.State = States.Empty;
		this.LineColor = Color.white;
		this.IsReady = true;
		this.IsRemovable = true;
	}
}
