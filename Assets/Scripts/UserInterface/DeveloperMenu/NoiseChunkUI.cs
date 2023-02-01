using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoiseChunkUI : MonoBehaviour {
    public Image MapImage;
    public Noise Noise;
    public NoiseMapInfoUI InfoUI;

    public void Render(string previewType = "Noise"){
        if (previewType == "Biome"){
            this.MapImage.sprite = SpriteHelper.CreateSprite(this.Noise.ToPreview(this.Noise.Config.Name));
        }
        else{
            this.MapImage.sprite = SpriteHelper.CreateSprite(this.Noise.ToTexture());
        }
    }
}
