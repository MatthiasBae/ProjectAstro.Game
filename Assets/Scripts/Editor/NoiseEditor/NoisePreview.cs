using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoiseConfig))]
public class NoisePreview : Editor{
	public Texture2D PreviewTexture;
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		if(GUILayout.Button("Show preview")){
			NoiseConfig noiseConfig = (NoiseConfig)target;

			var noise = Noise.Create(noiseConfig, noiseConfig.Seed, noiseConfig.Scale, 256, 256, Vector2.zero);
			this.PreviewTexture = noise.ToTexture();
		}
		GUILayout.Label(this.PreviewTexture);
	}
}
