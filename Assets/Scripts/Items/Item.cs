using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item {
    public ItemConfig Config;

    public static Item Create(ItemConfig config) {
        return new Item() {
            Config = config
        };
    }
}
