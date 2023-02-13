using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoiseConfig", menuName = "Configs/NoiseConfig")]
public class NoiseConfig : ScriptableObject {
    public string Name;
    public int Seed;
    public int Scale;
    public int Octaves;
    public float Persistance;
    public float Lacunarity;

    public float WeightFactor;
}
