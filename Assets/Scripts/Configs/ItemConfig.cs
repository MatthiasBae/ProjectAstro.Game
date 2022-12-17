using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/ItemConfig", order = 0)]
public class ItemConfig : ScriptableObject {
    public string Name;
    public float Weight;
    public Sprite SpriteUI;
    public Sprite Sprite;

    public ItemInventoryConfig Inventory;
}
