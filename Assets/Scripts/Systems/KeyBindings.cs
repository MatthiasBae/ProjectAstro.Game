using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyBindings {
    public KeyCode WalkUp = KeyCode.W;
    public KeyCode WalkDown = KeyCode.S;
    public KeyCode WalkLeft = KeyCode.A;
    public KeyCode WalkRight = KeyCode.D;
    public KeyCode Interact = KeyCode.E;
    public KeyCode Attack = KeyCode.Mouse0;
    public KeyCode Aim = KeyCode.Mouse1;
    public KeyCode Inventory = KeyCode.Tab;
    public KeyCode Menu = KeyCode.Escape;
    public KeyCode Debug = KeyCode.F1; 
}
