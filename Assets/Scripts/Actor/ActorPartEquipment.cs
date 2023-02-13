using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActorPartEquipment : MonoBehaviour {
	public InventoryConfig SlotConfig;
	public EquipmentSlot Slot;

	public void Initialize(){
		if(this.SlotConfig is null) {
			return;
		}
		this.Slot = new EquipmentSlot(this.SlotConfig);
	}
}
