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

    public const float TickRate = 1.0f;
    public const float TimeSystemTickRate = 10.0f;

    private void Update() {
        var deltaTime = Time.deltaTime;
        
        TickManager.Timer += deltaTime;
        TickManager.TimeSystemTimer += deltaTime;
        
        if(TickManager.Timer >= TickRate) {
            TickManager.OnTick?.Invoke();
            TickManager.Tick++;
            TickManager.Timer -= TickRate;
        }

        if(TickManager.TimeSystemTimer >= TimeSystemTickRate) {
            TickManager.OnTimeSystemTick?.Invoke();
            TickManager.TimeSystemTick++;
            TickManager.TimeSystemTimer -= TimeSystemTickRate;
        }
    }
}
