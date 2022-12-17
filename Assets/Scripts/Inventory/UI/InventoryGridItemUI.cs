using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridItemUI : MonoBehaviour {
    
    [SerializeField]
    private Image Image;

    [SerializeField]
    private RectTransform RectTransform;

    public InventoryItem InventoryItem;
}
