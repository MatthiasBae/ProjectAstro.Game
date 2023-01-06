using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetector : MonoBehaviour {
    //@Notiz: Wenn Performance hier eventuell mal nicht gut sein sollte, kann man auch eine Alternative mit Physics.OverlapSphere benutzen

    public List<DropItem> Items;
    public Inventory Inventory;

    public Action<DropItem> ItemFound;
    public Action<DropItem> ItemLost;

    private void Start() {
        this.RegisterEvents();
    }

    private void RegisterEvents() {
        this.ItemFound += (dropItem) => {
            var item = dropItem.Item;

            Debug.Log($"Item hinzugefügt:{this.Inventory.TryAddItem(item, 1)}");
        };

        this.ItemLost += (dropItem) => {
            var item = dropItem.Item;

            Debug.Log($"Item entfernt:{this.Inventory.RemoveItem(item)}");
        };
    }


    private void OnTriggerEnter2D(Collider2D collider) {
        var gameObject = collider.gameObject;
        var dropItem = gameObject.GetComponent<DropItem>();
        
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
            this.ItemLost?.Invoke(dropItem);
        }
    }
}
