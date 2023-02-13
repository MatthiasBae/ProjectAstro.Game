using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[Serializable]
public class MapGenerator {
    public List<BiomeConfig> BiomeConfigs;
    public List<NoiseConfig> NoiseConfigs;
    public List<TerrainLayer> TerrainLayers;
    
    //public NoiseStore NoiseStore;
    public NoiseChunkStore NoiseChunkStore;
    //public NoiseStore NoiseStore;

    public int GlobalSeed = 1;
    public int GlobalScale;

    public void Start() {
        this.NoiseChunkStore = new NoiseChunkStore();
    }

    public void CreateNoise(Chunk chunk){
        foreach (var noiseConfig in this.NoiseConfigs){
            var seed = noiseConfig.Seed == 0 ? this.GlobalSeed : noiseConfig.Seed;
            var scale = noiseConfig.Scale == 0 ? this.GlobalScale : noiseConfig.Scale;
            
            var chunkCount = ChunkManager.Instance.ChunkCount;
            var chunkSize = ChunkManager.Instance.ChunkSize;
            var noiseLocalSector = NoiseHelper.ChunkLocalSectorToNoiseLocalSector(chunk.LocalSector, chunkCount);
            
            Debug.Log($"Chunk Sektor: {chunk.LocalSector} || Noise Sektor: {noiseLocalSector}");
            var noise = Noise.Create(noiseConfig, seed, scale,  (int)chunk.Size.x, (int)chunk.Size.x, chunk.Sector * chunkSize);
            noise.Sector = chunk.Sector;
            this.NoiseChunkStore.AddNoiseChunk(noiseConfig.Name, chunk.Sector, noise);
        }
    }
    
    public void RemoveNoise(Chunk chunk) {
        this.NoiseChunkStore.RemoveNoiseChunk("Temperature", chunk.Sector);
        this.NoiseChunkStore.RemoveNoiseChunk("Humidity", chunk.Sector);
        this.NoiseChunkStore.RemoveNoiseChunk("Vegetation", chunk.Sector);
    }

    public BiomeConfig GetBiomeConfigAtPosition(Vector2 chunkSector, Vector2Int chunkField) {
        //@TODO: Eventuell cache f�r die BiomeConfigs erstellen, da diese sich nicht �ndern
  
        var temperatureNoiseChunk = this.NoiseChunkStore.GetNoiseChunk("Temperature", chunkSector);
        var humidityNoiseChunk = this.NoiseChunkStore.GetNoiseChunk("Humidity", chunkSector);
        var vegetationNoiseChunk = this.NoiseChunkStore.GetNoiseChunk("Vegetation", chunkSector);

        var temperatureNoiseValue = temperatureNoiseChunk.Map[chunkField.x, chunkField.y];
        var humidityNoiseValue = humidityNoiseChunk.Map[chunkField.x, chunkField.y];
        var vegetationNoiseValue = vegetationNoiseChunk.Map[chunkField.x, chunkField.y];
        
        var closestBiome = "";
        var closestDistance = float.MaxValue;

        foreach(var biomeConfig in this.BiomeConfigs) {
            var temperatureDistance = (temperatureNoiseValue - biomeConfig.Temperature) * (temperatureNoiseValue - biomeConfig.Temperature)  * temperatureNoiseChunk.Config.WeightFactor;
            var humidityDistance = (humidityNoiseValue - biomeConfig.Humidity) * (humidityNoiseValue - biomeConfig.Humidity) * humidityNoiseChunk.Config.WeightFactor;
            var vegetationDistance = (vegetationNoiseValue - biomeConfig.Vegetation) * (vegetationNoiseValue - biomeConfig.Vegetation) * vegetationNoiseChunk.Config.WeightFactor;

            var overallDistance = Mathf.Sqrt(temperatureDistance + humidityDistance + vegetationDistance);
            if (overallDistance < closestDistance) {
                closestDistance = overallDistance;
                closestBiome = biomeConfig.Name;
            }
        }
        return this.GetBiomeConfig(closestBiome);
    }
    public BiomeConfig GetBiomeConfigAtPosition(string layer, Vector2 chunkSector, Vector2Int chunkField) {
        var layerNoiseChunk = this.NoiseChunkStore.GetNoiseChunk(layer, chunkSector);
        var layerNoiseValue = layerNoiseChunk.Map[chunkField.x, chunkField.y];

        var closestBiome = "";
        var closestDistance = float.MaxValue;

        foreach(var biomeConfig in this.BiomeConfigs) {
            var layerNoiseDistance = (layerNoiseValue - biomeConfig.Vegetation) * (layerNoiseValue - biomeConfig.Vegetation) * layerNoiseChunk.Config.WeightFactor;

            var overallDistance = Mathf.Sqrt(layerNoiseDistance );//+ humidityDistance + vegetationDistance);
            if (overallDistance < closestDistance) {
                closestDistance = overallDistance;
                closestBiome = biomeConfig.Name;
            }
        }
        return this.GetBiomeConfig(closestBiome);
    }
    public BiomeConfig GetBiomeConfig(string name) {
        foreach(var biomeConfig in this.BiomeConfigs) {
            if(biomeConfig.Name == name) {
                return biomeConfig;
            }
        }
        return null;
    }

    public void GetChunkTerrainData(Chunk chunk){
        foreach (var chunkField in chunk.Fields){

            var fieldTerrain = this.GetFieldTerrain(chunk.Sector, chunkField.PositionInChunk);
            chunkField.FieldController = new FieldController(fieldTerrain);
        }
    }
    
    public FieldTerrain GetFieldTerrain(Vector2 chunkSector, Vector2Int chunkField) {
        var biomeConfig = this.GetBiomeConfigAtPosition(chunkSector, chunkField);
        
        var fieldTerrain = new FieldTerrain(){
            BiomeConfig = biomeConfig
        };
        return fieldTerrain;
    }

    public TerrainLayer GetTerrainLayer(string layerName){
        foreach (var layer in this.TerrainLayers){
            if(layer.Name == layerName){
                return layer;
            }
        }

        return null;
    }
}
