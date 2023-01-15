using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetector : MonoBehaviour {
    //@Notiz: Wenn Performance hier eventuell mal nicht gut sein sollte, kann man auch eine Alternative mit Physics.OverlapSphere benutzen

    public GameObject DropItemPrefab;

    public List<DropItem> Items;
    public List<InventoryItem> ItemsIgnored;
    public Inventory Inventory;

    public Action<DropItem> ItemFound;
    public Action<DropItem> ItemLost;

    private void Start() {
        this.RegisterEvents();
    }

    private void RegisterEvents() {
        this.ItemFound += (dropItem) => {
            var inventoryItem = dropItem.InventoryItem;

            this.Inventory.TryAddItem(inventoryItem);
        };

        this.ItemLost += (dropItem) => {
            var inventoryItem = dropItem.InventoryItem;
            
            this.Inventory.RemoveItem(inventoryItem);
        };

        //@TODO: Wenn im Inventory das Item nicht mehr vorhanden ist, dann soll es auch aus der Welt entfernt werden
        this.Inventory.ItemRemoved += (inventoryItem) => {
            foreach(var dropItem in this.Items) {
                if(dropItem.InventoryItem == inventoryItem) {
                    GameObject.Destroy(dropItem.gameObject);
                    break;
                }
            }
        };

        this.Inventory.ItemAddedTry += (inventoryItem, position, added) => {
            if(!added) {
                return;
            }

            if(this.DropItemExists(inventoryItem)) {
                return;
            }

            this.AddDropItem(inventoryItem);
        };
    }

    private bool DropItemExists(InventoryItem inventoryItem) {
        foreach(var dropItem in this.Items) {
            if(dropItem.InventoryItem == inventoryItem) {
                return true;
            }
        }
        return false;
    }

    private void AddDropItem(InventoryItem inventoryItem) {
        var dropItem = DropItem.Create(inventoryItem, this.transform.position, this.DropItemPrefab);
        this.Items.Add(dropItem);
        this.ItemsIgnored.Add(inventoryItem);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        var gameObject = collider.gameObject;
        var dropItem = gameObject.GetComponent<DropItem>();
        var inventoryItem = dropItem.InventoryItem;
        
        if(this.ItemsIgnored.Contains(inventoryItem)) {
            return;
        }

        if(dropItem != null) {
            this.Items.Add(dropItem);
            this.ItemFound?.Invoke(dropItem);
        }
    }
    private void OnTriggerExit2D(Collider2D collider) {
        var gameObject = collider.gameObject;
        var dropItem = gameObject.GetComponent<DropItem>();
        
        if(dropItem != null) {
            this.Items.Remove(dropItem);
            this.ItemsIgnored.Remove(dropItem.InventoryItem);
            this.ItemLost?.Invoke(dropItem);
        }
    }
}
