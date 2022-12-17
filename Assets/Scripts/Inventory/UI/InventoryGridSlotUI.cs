using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridSlotUI : MonoBehaviour{
    
    [SerializeField]
    private Image Image;

    public void SetColor(Color color) {
        this.Image.color = color;
    }
}
