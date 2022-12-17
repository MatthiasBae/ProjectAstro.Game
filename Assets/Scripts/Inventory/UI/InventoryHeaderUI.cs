using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryHeaderUI : MonoBehaviour {

    [SerializeField]
    private TMP_Text Name;

    [SerializeField]
    private TMP_Text Weight;
    

    public void SetWeight(float weight, float maxWeight) {
        this.Weight.text = $"Weight: {weight}Kg / {maxWeight}Kg";
    }

    public void SetName(string name) {
        this.Name.text = name;
    }
}
