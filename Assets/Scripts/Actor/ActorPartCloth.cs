using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPartCloth : ActorPart {
	public ActorPartEquipment Equipment;
	
	private void Awake(){
		this.Equipment.Initialize();
	}
}
