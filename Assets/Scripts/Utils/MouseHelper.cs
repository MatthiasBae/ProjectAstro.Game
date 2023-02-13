using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHelper {
    public static Vector2 MousePosition {
        get => Input.mousePosition;
    }
    
    public Vector2 MouseWorldPosition {
        get => Camera.main.ScreenToWorldPoint(MouseHelper.MousePosition);
    }
}
