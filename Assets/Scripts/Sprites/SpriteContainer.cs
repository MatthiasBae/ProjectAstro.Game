using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteContainer", menuName = "Sprites/SpriteContainer")]
public class SpriteContainer : ScriptableObject {
    public List<SpriteContainerCategory> Categories = new List<SpriteContainerCategory>();
    public SpriteContainerCategory AddCategory(string categoryName) {
        var category = new SpriteContainerCategory() {    
            Name = categoryName
        };
        this.Categories.Add(category);
        return category;
    }
    
    public SpriteContainerCategory GetCategory(string name) {
        foreach(var category in this.Categories) {
            if(category.Name == name) {
                return category;
            }
        }
        return null;
    }
    
    public void AddSprite(string categoryName, Sprite sprite) {
        var spriteCategory = this.GetCategory(categoryName);
        if(spriteCategory == null) {
            spriteCategory = this.AddCategory(categoryName);
        }
        spriteCategory.Sprites.Add(sprite);
    }
    
    public Sprite GetSprite(string categoryName, int frame) {
        var category = this.Categories.Find(item => item.Name == categoryName);
        if(category == null) {
            return null;
        }
        
        if(category.Sprites.Count < frame) {
            return null;
        }
        
        var sprite = category.Sprites[frame];
        if(sprite == null) {
            return null;
        }
        return sprite;
    }
}
