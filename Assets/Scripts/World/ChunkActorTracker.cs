using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChunkActorTracker {
    public Actor Player;
    public Vector2 Sector;

    public bool SwitchedChunk;

    public void RegisterPlayer(Actor player) {
        this.Player = player;
    }

    public void UpdatePlayerSector() {
        var currentSector = ChunkHelper.WorldToSector(this.Player.transform.position, ChunkManager.Instance.ChunkSize);
        if(currentSector != this.Sector) {
            //Debug.Log($"Chunk gewechselt von {this.Sector} zu {currentSector}");
            this.SwitchedChunk = true;
            this.Sector = currentSector;
            
            return;
        }
        this.SwitchedChunk = false;
    }
}
