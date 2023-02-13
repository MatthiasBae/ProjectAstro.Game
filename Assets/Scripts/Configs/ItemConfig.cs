using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/ItemConfig", order = 0)]
public class ItemConfig : ScriptableObject {
    public string Name;
    public float Weight;

    public InventoryConfig InventoryConfig;
    public Vector2 SlotSize;
    public int MaxStackSize;
    
    public Sprite SpriteUI;
    public Sprite Sprite;
    
    public bool IsStackable {
        get => this.MaxStackSize > 1;
    }
    public bool HasInventory {
        get => this.InventoryConfig != null;
    }
    public bool IsEquipment;
}
