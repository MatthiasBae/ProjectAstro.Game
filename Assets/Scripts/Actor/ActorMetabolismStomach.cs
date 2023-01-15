using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;


public class ActorMetabolismStomach {
    public ActorMetabolism ActorMetabolism;
    
    public List<ActorMetabolismStomachContent> Contents;
    public float Calories {
        get {
            float calories = 0.0f;
            foreach(var content in this.Contents) {
                calories += content.Calories;
            }
            return calories;
        }
    }

    public ActorMetabolismStomach(ActorMetabolism metabolism) {
        this.ActorMetabolism = metabolism;
        this.Contents = new List<ActorMetabolismStomachContent>();
    }

    public void AddMeal(ActorMetabolismStomachContent content) {
        Debug.Log($"Adding meal to stomach:{content.Calories}");
        this.Contents.Add(content);
    }

    public void RemoveMeal(ActorMetabolismStomachContent content) {
        this.Contents.Remove(content);
    }

    //@TODO: Hier prüfen warum die Kalorien nicht ganz den Gesamtkalorien von der Mahlzeit entsprechen wenn ich es über die Zeit verteilt abziehe.
    public void MetabolizeContent(DateTime currentTime) {
        var listToRemove = new List<ActorMetabolismStomachContent>();

        foreach(var content in this.Contents) {

            if(currentTime >= content.ProteinsMetabolizedAt && content.ProteinMetabolized == false) {
                Debug.Log($"Addiere Kalorien von Protein");
                this.ActorMetabolism.CaloriesDeposit += content.ProteinCalories;
                content.ProteinMetabolized = true;
            }

            if(currentTime >= content.CarbohydratesMetabolizedAt && content.CarbohydratesMetabolized == false) {
                Debug.Log($"Addiere Kalorien von Kohlenhydrate");
                this.ActorMetabolism.CaloriesDeposit += content.CarbohydrateCalories;
                content.CarbohydratesMetabolized = true;
            }

            if(currentTime >= content.FatsMetabolizedAt && content.FatsMetabolized == false) {
                Debug.Log($"Addiere Kalorien von Fette");
                this.ActorMetabolism.CaloriesDeposit += content.FatCalories;
                content.FatsMetabolized = true;
            }

            if(content.ProteinMetabolized && content.CarbohydratesMetabolized && content.FatsMetabolized) {
                Debug.Log($"Entferne Mahlzeit");
                listToRemove.Add(content);
            }
        }

        foreach(var item in listToRemove) {
            this.RemoveMeal(item);
        }
    }
}
