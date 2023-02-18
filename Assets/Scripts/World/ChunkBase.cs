using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkBase {
	
	public Vector2 Size;
	public Vector2 Sector;
	public Vector2 LocalSector{
		get => this.Sector - ChunkManager.Instance.ActorTracker.Sector;
	}
	public ChunkEvolutionBase Evolution;
	public ChunkRenderer Renderer;
	public ChunkActorTracker ActorTracker;
	public List<ChunkField> Fields;

	public bool IsRemovable;
	public Color LineColor;
	public bool IsInRange {
		get => this.IsInPlayerRange(this.ActorTracker);
	}
	
	public float LifeDuration;
	public float Timer;
	public bool TimerFinished{
		get => this.Timer >= this.LifeDuration;
	}


	public ChunkBase(ChunkActorTracker actorTracker){
		this.ActorTracker = actorTracker;
		this.Evolution = new ChunkEvolutionEmpty(this);
		this.Renderer = new ChunkRenderer(this);
	}
	
	public static ChunkBase Create(ChunkActorTracker actorTracker, Vector2 size, Vector2 sector, float duration, ChunkEvolutionBase.States state) {
		var chunk = new ChunkBase(actorTracker){
			Size = size,
			Sector = sector,
			LifeDuration = duration
		};
		switch(state){
			case ChunkEvolutionBase.States.Empty:
				chunk.Evolution = new ChunkEvolutionEmpty(chunk);
				break;
			case ChunkEvolutionBase.States.Create:
				chunk.Evolution = new ChunkEvolutionCreated(chunk);
				break;
			case ChunkEvolutionBase.States.Prepare:
				chunk.Evolution = new ChunkEvolutionPrepared(chunk);
				break;
			case ChunkEvolutionBase.States.Render:
				chunk.Evolution = new ChunkEvolutionRendered(chunk);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(state), state, null);
		}
		return chunk;
	}
	
	public void Update(){
		if (this.IsInRange){
			this.ResetTimer();
		}
		else{
			this.UpdateTimer();
		}

		this.Evolution.Update();

	}
	public void UpdateTimer() {
		if(this.Timer <= this.LifeDuration) {
			this.Timer += Time.deltaTime;
			return;
		}
	}

	public void ResetTimer() {
		this.Timer = 0;
	}

	public bool IsInPlayerRange(ChunkActorTracker chunkActorTracker) {
		Debug.Log(chunkActorTracker);
		var halfChunkCount = ChunkManager.Instance.ChunkCount / 2;
		var chunkIsInRange = this.Sector.x >= chunkActorTracker.Sector.x - halfChunkCount.x &&
		                     this.Sector.x <= chunkActorTracker.Sector.x + halfChunkCount.x &&
		                     this.Sector.y >= chunkActorTracker.Sector.y - halfChunkCount.y &&
		                     this.Sector.y <= chunkActorTracker.Sector.y + halfChunkCount.y;
		return chunkIsInRange;

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
