using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;


public class Noise {
    public Vector2 Sector;
    public float[,] Map;
    public NoiseConfig Config;
    
    //@TODO: Scale noch auslagern als globalen wert?
    public static Noise Create(NoiseConfig config, int seed, int scale, int width, int height, Vector2 offset) {
        var noise = new Noise() {
            Config = config
        };
        noise.Calculate(seed, scale, width, height, offset);
        return noise;
    }

    public void Calculate(int seed, int scale, int width, int height, Vector2 offset) {
        this.Map = Noise.Generate(seed, width, height, scale, this.Config.Octaves, this.Config.Persistance, this.Config.Lacunarity, offset);
    }

    public static float[,] Generate(int seed, int width, int height, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
        Debug.Log($"NoiseOffset: {offset}");
        float[,] noiseMap = new float[width, height];
        System.Random rng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for(int i = 0; i < octaves; i++) {
            float offsetX = rng.Next(-100000, 100000) + offset.x;
            float offsetY = rng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
        if(scale <= 0) {
            scale = 0.0001f;
        }

        float maxNoiseHeight = 1f;//float.MinValue;
        float minNoiseHeight = -1f;//float.MaxValue;
        
        //Damit der Ursprung des Perlin Noise nicht in der Ecke ist
        float halfWidth = width / 2f;
        float halfHeight = height / 2f;
        
        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                float amplitude = 1f;
                float frequency = 1.5f;
                float noiseHeight = 0;

                for(int i = 0; i < octaves; i++) {
                    float sampleX = (x - halfWidth + octaveOffsets[i].x) / scale * frequency;
                    float sampleY = (y - halfHeight + octaveOffsets[i].y) / scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;
                    //Debug.Log("Noise: PerlinValue: " + perlinValue + " * Amplitude: " + amplitude + " = NoiseHeight:" + noiseHeight + "");
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                // if(noiseHeight > maxNoiseHeight) {
                //     maxNoiseHeight = noiseHeight;
                // }
                // else if(noiseHeight < minNoiseHeight) {
                //     minNoiseHeight = noiseHeight;
                // }
                
                noiseMap[x, y] = noiseHeight;
            }
        }
        //Debug.Log($"Noise MAX: {maxNoiseHeight} MIN: {minNoiseHeight}");
        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
        
        return noiseMap;
    }
    public Texture2D ToTexture() {
        int width = Map.GetLength(0);
        int height = Map.GetLength(1);
        Texture2D texture = new Texture2D(width, height);
        Color[] colors = new Color[width * height];
        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                colors[y * width + x] = Color.Lerp(Color.black, Color.white, Map[x, y]);
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
        return texture;
    }

    public Texture2D ToPreview(string layer = ""){
        int width = Map.GetLength(0);
        int height = Map.GetLength(1);
        Texture2D texture = new Texture2D(width, height);
        Color[] colors = new Color[width * height];
        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                if (layer != ""){
                    colors[y * width + x] = WorldGenerator.Instance.MapGenerator.GetBiomeConfigAtPosition(layer, this.Sector, new Vector2Int(x, y)).MapColor;
                }
                else{
                    colors[y * width + x] = WorldGenerator.Instance.MapGenerator.GetBiomeConfigAtPosition(this.Sector, new Vector2Int(x, y)).MapColor;
                }
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
        return texture;
    }
}
