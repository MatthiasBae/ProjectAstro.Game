using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryConfig", menuName = "Configs/InventoryConfig")]
public class InventoryConfig : ScriptableObject {
    public string Name;
    public int MaxWeight;
    public int SlotsY;
    public int SlotsX;
}
