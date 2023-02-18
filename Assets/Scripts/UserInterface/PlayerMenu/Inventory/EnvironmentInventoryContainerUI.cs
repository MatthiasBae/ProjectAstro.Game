using UnityEngine;

public class EnvironmentInventoryContainerUI : MonoBehaviour {
	public InventoryUI InventoryUI;

	public ItemDetector ItemDetector;
	
	private void Start(){
		this.InventoryUI.InitializeInventory(this.ItemDetector.Inventory);
		this.RegisterEvents();
	}

	private void RegisterEvents(){
		//this.ItemDetector.ItemFound += this.InventoryUI.Tr;
	}
}
