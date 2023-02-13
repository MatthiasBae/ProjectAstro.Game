using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class NoiseMapInfoUI : MonoBehaviour {
    public GameObject NoiseMapUIList;
    public GameObject BiomePreviewUIList;
    
    public GameObject NoiseMapUIPrefab;
    public GameObject BiomePreviewUIPrefab;

    private float timer = 5;

    private List<Vector2> RenderedChunks = new List<Vector2>();
    
    public void Update(){
        timer -= Time.deltaTime;
        if(timer<=0){
            timer = 5;
            this.UpdateRenderedChunks();
            
            this.ClearNoiseMapUIs();
            this.CreateNoiseMapUIs();
            
            this.ClearBiomePreviewUIs();
            this.CreateBiomePreviewUIs();
        }
    }
    
    public void CreateNoiseMapUIs() {
        var noiseMaps = WorldGenerator.Instance.MapGenerator.NoiseChunkStore.NoiseLayers;

        foreach(var layer in noiseMaps) {
            var noiseUIGameObject = Instantiate(this.NoiseMapUIPrefab, this.NoiseMapUIList.transform);
            var noiseUI = noiseUIGameObject.GetComponent<NoiseUI>();
            var noises = layer.Value.NoiseMaps;
            
            var inRangeNoises = noises.Where(item=>this.RenderedChunks.Contains(item.Key)).ToDictionary(item=>item.Key, item=>item.Value);
            noiseUI.Noises = inRangeNoises;
            noiseUI.Render();
        }
    }

    public void UpdateRenderedChunks(){
        this.RenderedChunks.Clear();
        var chunks = WorldGenerator.Instance.ChunkManager.Store.Chunks;
        foreach(var chunk in chunks) {
            if (chunk.Value.State.State == ChunkStateBase.States.Rendered){
                this.RenderedChunks.Add(chunk.Key);
            }
        }
    }
    public void ClearNoiseMapUIs() {
        foreach(Transform child in this.NoiseMapUIList.transform) {
            Destroy(child.gameObject);
        }
    }

    public void CreateBiomePreviewUIs(){
        var noiseMaps = WorldGenerator.Instance.MapGenerator.NoiseChunkStore.NoiseLayers;

        foreach(var layer in noiseMaps) {
            var noiseUIGameObject = Instantiate(this.NoiseMapUIPrefab, this.BiomePreviewUIList.transform);
            var noiseUI = noiseUIGameObject.GetComponent<NoiseUI>();
            var noises = layer.Value.NoiseMaps;
            
            var inRangeNoises = noises.Where(item=>this.RenderedChunks.Contains(item.Key)).ToDictionary(item=>item.Key, item=>item.Value);
            noiseUI.Noises = inRangeNoises;
            noiseUI.Render("Biome");
        }
    }
    
    public void ClearBiomePreviewUIs() {
        foreach(Transform child in this.BiomePreviewUIList.transform) {
            Destroy(child.gameObject);
        }
    }
}
