using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInventoryConfig {
    public Vector2 Size;
    public int MaxStackSize;
    public float Weight;
    public bool IsStackable {
        get => this.MaxStackSize > 1;
    }
}
