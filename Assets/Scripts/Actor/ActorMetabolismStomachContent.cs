using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActorMetabolismStomachContent {
    public DateTime DateEaten;
    
    public bool ProteinMetabolized;
    public bool CarbohydratesMetabolized;
    public bool FatsMetabolized;

    public DateTime ProteinsMetabolizedAt;
    public DateTime CarbohydratesMetabolizedAt;
    public DateTime FatsMetabolizedAt;
    
    public float Calories {
        get => this.ProteinCalories + this.CarbohydrateCalories + this.FatCalories;
    }
    public float Proteins;
    public float Carbohydrates;
    public float Fats;
    
    public float ProteinCalories {
        get => this.Proteins * 4.1f;
    }

    public float CarbohydrateCalories {
        get => this.Carbohydrates * 4.1f;
    }

    public float FatCalories {
        get => this.Fats * 9.3f;
    }

    public static ActorMetabolismStomachContent Create(DateTime dateTime, float proteins, float carbohydrates, float fats) {
        return new ActorMetabolismStomachContent() {
            DateEaten = dateTime,
            Proteins = proteins,
            Carbohydrates = carbohydrates,
            Fats = fats,
            ProteinsMetabolizedAt = dateTime.AddHours(3.5f),
            CarbohydratesMetabolizedAt = dateTime.AddHours(1.5f),
            FatsMetabolizedAt = dateTime.AddHours(4.5f)
        };
    }
}
