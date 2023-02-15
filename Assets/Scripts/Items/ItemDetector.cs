using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDetector : MonoBehaviour {
    [SerializeField]
    private GameObject DropItemPrefab;
    public InventoryConfig InventoryConfig;
    public ItemConfig CreateTestConfig;
    public ItemConfig CreateInventoryItemConfig;
    
    public ItemContainerBase Inventory;
    private List<DropItem> ItemsFound;
    private List<Item> ItemsIgnored;
    public event Action<DropItem> ItemFound;
    public event Action<DropItem> ItemLost;
    
    private void Awake(){
        this.Initialize();
    }

    public void Start(){
        this.RegisterEvents();
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            var itemObj = DropItem.Create(this.DropItemPrefab, (Vector2)this.transform.position + new Vector2(4, 4), Item.Create(this.CreateTestConfig, 1));
            var itemInventory = itemObj.Item.Inventory;
            itemInventory.TryAddItem(Item.Create(this.CreateInventoryItemConfig, 1));
        }
    }
        
    private void Initialize(){
        this.ItemsFound = new List<DropItem>();
        this.ItemsIgnored = new List<Item>();
        this.Inventory = new Inventory(this.InventoryConfig);
        
    }
    
    public void RegisterEvents(){
        this.ItemFound += this.TryAddItem;
        this.ItemLost += this.TryRemoveItem;
        
        this.Inventory.ItemRemoved += this.DestroyDropItem;
        this.Inventory.ItemAdded += this.TryCreateDropItem;
    }
    
    public void TryAddItem(DropItem dropItem){
        var item = dropItem.Item;
        if(this.ItemsIgnored.Contains(item)){
            return;
        }
        this.ItemsIgnored.Add(item);
        
        var added = this.Inventory.TryAddItem(item);
        if(added){
            
        }
    }
    
    public void TryRemoveItem(DropItem dropItem){
        var item = dropItem.Item;
 
        var removed = this.Inventory.TryRemoveItem(item);
        if(removed){
            this.ItemsIgnored.Remove(item);
        }
    }
    
    public void DestroyDropItem(Item item, Vector2 position){
        if(!this.ItemsIgnored.Contains(item)){
            return;
        }

        foreach (var dropItem in this.ItemsFound){
            if(dropItem.Item == item){
                this.ItemsFound.Remove(dropItem);
                this.ItemsIgnored.Remove(dropItem.Item);
                Destroy(dropItem.gameObject);
                return;
            }
        }
    }
    
    public void TryCreateDropItem(Item item, Vector2 position){
        if(this.ItemsIgnored.Contains(item)){
            return;
        }
        var dropItem = DropItem.Create(this.DropItemPrefab, this.transform.position, item);
        this.ItemsFound.Add(dropItem);
        this.ItemsIgnored.Add(item);
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        var dropItem = other.GetComponent<DropItem>();
        if(dropItem != null){
            if(!this.ItemsFound.Contains(dropItem)){
                this.ItemsFound.Add(dropItem);
                this.ItemFound?.Invoke(dropItem);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other){
        var dropItem = other.GetComponent<DropItem>();
        if(dropItem != null){
            if(this.ItemsFound.Contains(dropItem)){
                this.ItemsFound.Remove(dropItem);
                this.ItemLost?.Invoke(dropItem);
            }
        }
    }
    
}
