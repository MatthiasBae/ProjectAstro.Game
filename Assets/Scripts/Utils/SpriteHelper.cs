using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHelper {
    public static Sprite CreateSprite(Texture2D texture) {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
    
    public static Texture2D CreateTexture(Color[,] colors) {
        var width = colors.GetLength(0);
        var height = colors.GetLength(1);
        
        var texture = new Texture2D(width, height);
        
        for(var x = 0; x < width; x++) {
            for(var y = 0; y < height; y++) {
                texture.SetPixel(x, y, colors[x, y]);
            }
        }
        
        texture.Apply();
        return texture;
    }
}
