using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NoiseUI : MonoBehaviour {
	public Dictionary<Vector2, Noise> Noises;
	public GridLayoutGroup NoiseChunkGrid;

	public GameObject NoiseChunkUIPrefab;
	public void Render(string previewType = "Noise"){
		var sortedNoises = this.Noises.OrderByDescending(item => item.Key.y)
			.ThenBy(item => item.Key.x);

		foreach (var noise in sortedNoises){
			var noiseChunkGameObject = Instantiate(this.NoiseChunkUIPrefab, this.transform);
			var noiseChunkUI = noiseChunkGameObject.GetComponent<NoiseChunkUI>();
			noiseChunkUI.Noise = noise.Value;
			noiseChunkUI.Render(previewType);
		}
	}
}
