using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteLoader {
    public static List<Texture2D> LoadAll(string path) => Resources.LoadAll<Texture2D>(path).ToList();
}
