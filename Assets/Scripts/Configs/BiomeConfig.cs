using UnityEngine;

[CreateAssetMenu(fileName = "Configs/BiomeConfig", menuName = "Configs/BiomeConfig")]
public class BiomeConfig : ScriptableObject {
	public string Name;
	public Color MapColor;
	public RuleTile RuleTile;
	
	public float Temperature;
	public float Humidity;
	public float Vegetation;
}
