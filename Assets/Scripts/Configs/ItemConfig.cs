using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/ItemConfig", order = 0)]
public class ItemConfig : ScriptableObject {
    public enum Categories {
        Weapon,
        Armor,
        Jacket,
        Hat,
        Pants,
        Shirt,
        Consumable
    }
    
    
    public string Name;
    public float Weight;
    public Categories Category;
    public string Description;
    
    public InventoryConfig InventoryConfig;
    public Vector2 Slots;
    public int MaxStackSize;
    
    public Sprite SpriteUI;
    public Sprite Sprite;
    
    public bool IsStackable {
        get => this.MaxStackSize > 1;
    }
    public bool HasInventory {
        get => this.InventoryConfig != null;
    }

    public bool IsEquipment{
        get{
            if(this.Category == Categories.Weapon 
               || this.Category == Categories.Armor
               || this.Category == Categories.Hat
               || this.Category == Categories.Jacket
               || this.Category == Categories.Pants
               || this.Category == Categories.Shirt) {
                    return true;
            }
            return false;
        }
    }
}
