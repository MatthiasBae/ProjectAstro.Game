using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public ItemConfig Config;
    public int Amount;

    public static Item Create(ItemConfig itemConfig, int amount = 1) {
        return new Item {
            Config = itemConfig,
            Amount = amount
        };
    }
}
