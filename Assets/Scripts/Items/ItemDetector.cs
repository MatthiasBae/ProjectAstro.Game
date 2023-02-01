using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDetector : MonoBehaviour {
    [SerializeField]
    private GameObject ItemDropPrefab;
    private List<Item> ItemsIgnored;
    private List<ItemDrop> ItemsFound;

    public Inventory Inventory;

    public event Action<ItemDrop> ItemFound;
    public event Action<ItemDrop> ItemLost;

    private void Start() {
        this.RegisterEvents();
    }

    private void RegisterEvents() {
        this.ItemFound += (itemDrop) => {
            var item = itemDrop.Item;

            this.Inventory.TryAddItem(item);
        };

        this.ItemLost += (itemDrop) => {
            var item = itemDrop.Item;

            this.Inventory.RemoveItem(item);
        };

        //@TODO: Wenn im Inventory das Item nicht mehr vorhanden ist, dann soll es auch aus der Welt entfernt werden
        this.Inventory.ItemRemoved += (inventoryItem) => {
            foreach(var itemDrop in this.ItemsFound) {
                if(itemDrop.Item == inventoryItem) {
                    GameObject.Destroy(itemDrop.gameObject);
                    break;
                }
            }
        };

        this.Inventory.ItemAddedTry += (item, position, added) => {
            if(!added) {
                return;
            }

            if(this.ItemDropExists(item)) {
                return;
            }

            this.AddItemDrop(item);
        };
    }

    private bool ItemDropExists(Item item) {
        foreach(var itemDrop in this.ItemsFound) {
            if(itemDrop.Item == item) {
                return true;
            }
        }
        return false;
    }

    private void AddItemDrop(Item item) {
        var dropItem = ItemDrop.Create(item, this.transform.position, this.ItemDropPrefab);
        this.ItemsFound.Add(dropItem);
        this.ItemsIgnored.Add(item);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        var itemDrop = collision.gameObject.GetComponent<ItemDrop>();

        if(itemDrop == null) {
            return;
        }

        var item = itemDrop.Item;
        if(this.ItemsIgnored.Contains(item)) {
            return;
        }

        this.ItemsFound.Add(itemDrop);
        this.ItemFound?.Invoke(itemDrop);
    }

    public void OnTriggerExit2D(Collider2D collision) {
        var itemDrop = collision.gameObject.GetComponent<ItemDrop>();

        if(itemDrop == null) {
            return;
        }

        var item = itemDrop.Item;

        this.ItemsIgnored.Remove(item);
        this.ItemsFound.Remove(itemDrop);

        this.ItemLost?.Invoke(itemDrop);
    }
}
