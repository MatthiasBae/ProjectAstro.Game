using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryInfoUI : MonoBehaviour {
    public TMP_Text NameText;
    public TMP_Text WeightText;

    public void SetNameText(string name) {
        this.NameText.text = name;
    }
    public void SetWeightText(float maxWeight, float actualWeight) {
        this.WeightText.text = $"Weight: {maxWeight}Kg / {actualWeight}Kg";
    }
    public void ResetInfo() {
        this.SetNameText("");
        this.SetWeightText(0, 0);
    }
}
