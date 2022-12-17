using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAnimationLayer {
    public SpriteContainer SpriteContainer;
    public SpriteRenderer SpriteRenderer;

    public void SetFrame(string category, int frame) {
        var sprite = this.GetFrame(category, frame);
        if(sprite == null) {
            return;
        }
        this.SpriteRenderer.sprite = sprite;
    }

    public void SetSortingOrder(int order) {
        this.SpriteRenderer.sortingOrder = order;
    }

    public Sprite GetFrame(string category, int frame) {
        var sprite = this.SpriteContainer.GetSprite(category, frame);
        return sprite;
    }
}
