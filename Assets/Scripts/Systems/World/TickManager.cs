using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour {
    public static event Action OnTick;
    public static event Action OnTimeSystemTick;

    public static float Timer;
    public static float TimeSystemTimer;

    public static float Tick;
    public static float TimeSystemTick;

    public float TickRate;
    public float TimeSystemTickRate;

    private void Update() {
        var deltaTime = Time.deltaTime;
        
        TickManager.Timer += deltaTime;
        TickManager.TimeSystemTimer += deltaTime;
        
        if(TickManager.Timer >= this.TickRate) {
            TickManager.OnTick?.Invoke();
            TickManager.Tick++;
            TickManager.Timer -= this.TickRate;
        }

        if(TickManager.TimeSystemTimer >= this.TimeSystemTickRate) {
            TickManager.OnTimeSystemTick?.Invoke();
            TickManager.TimeSystemTick++;
            TickManager.TimeSystemTimer -= this.TimeSystemTickRate;
        }
    }
}
