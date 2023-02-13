using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryInfoUI : MonoBehaviour {
    public TMP_Text Name;
    public TMP_Text Category;
    public TMP_Text Weight;
    
    public void SetCategory(string category){
        this.Category.text = category;
    }
    public void SetName(string name){
        this.Name.text = name;
    }
    public void SetWeight(float weight, float maxWeight){
        this.Weight.text = $"Weight: {weight}Kg. / {maxWeight}Kg.";
    }
}

