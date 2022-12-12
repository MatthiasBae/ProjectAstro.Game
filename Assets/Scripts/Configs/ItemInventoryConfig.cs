using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInventoryConfig {
    public int SlotsX;
    public int SlotsY;
    public int MaxStackSize;
    public float Weight;
    public bool IsStackable {
        get => this.MaxStackSize > 1;
    }
}
