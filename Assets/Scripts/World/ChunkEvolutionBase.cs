using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChunkEvolutionBase {
	public enum States {Empty, Create, Prepare, Render};
	public enum ProcessStates {Empty, Entered, Updating, Exited};
	
	public States State;
	public ProcessStates ProcessState;
	public bool IsReady;

	public ChunkBase Chunk;
	public ChunkEvolutionBase(ChunkBase chunk){
		this.State = States.Empty;
		this.ProcessState = ProcessStates.Empty;
		this.IsReady = false;
		this.Chunk = chunk;
	}
	

	//@TODO: Only build if you are not downgrading the chunk
	public void Start(){
		this.ProcessState = ProcessStates.Entered;
		this.Build();
		this.IsReady = true;
	}

	public virtual void Update(){
		this.ProcessState = ProcessStates.Updating;
	}
	
	public void Exit(){
		this.ProcessState = ProcessStates.Exited;
	}

	public virtual void Build(){
		
	}
	
	public virtual void Dismantle(){
		
	}
	
	//@TODO: Check why it does not wait 30 seconds after the chunk will regress to the previous state
	public void SwitchEvolutionState(ChunkBase chunk, ChunkEvolutionBase evolutionState, bool dismantle = false){
		if (dismantle){
			chunk.Evolution.Dismantle();
		}
		
		chunk.ResetTimer();
		chunk.Evolution.Exit();
		chunk.Evolution = evolutionState;
		chunk.Evolution.Start();
	}
}
