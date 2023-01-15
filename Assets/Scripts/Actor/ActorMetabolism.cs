using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ActorMetabolism {

    //Grundumsatz berechnen anhand der Harris Benedict Formel
    //Männer: BMR = 66 + (13.7 x Gewicht in Kilogramm) + (5 x Körpergröße in cm) – (6.8 x Alter in Jahren)
    //Frauen: BMR = 655 + (9.6 X Gewicht in Kilogramm) + (1.8 X Körpergröße in cm) – (4.7 x Alter in Jahren).

    //Aktivitätskalorien berechnen mithilfe des PAL Faktors
    //PAL Faktor = 1.2 (Sitzende Person) bis 1.9 (extrem aktive Person)


    //-------- IDEE --------
    //1. Verbrannte Kalorien abhängig machen von Inventargewicht, State des Spielers (Stehen, Laufen, Rennen) -> höherer PAL Faktor
    //----------------------

    public float BMR;
    public float AMR;
    
    public float BMRPerMinute;
    public float AMRPerMinute;

    public float CaloriesDeposit;

    public ActorMetabolismStomach Stomach;

    [SerializeField]
    private float CaloriesDifferenceTolerance = 75.0f;
    [SerializeField]
    private float CaloriesPerKilo = 7000.0f;

    [SerializeField]
    private float MinutesInCaloriesDifference;
    [SerializeField]
    private float InfluenceBodyDuration;
    [SerializeField]
    private float InfluenceBodyThreshold;

    private ActorBody ActorBody;
    private ActorProfile ActorProfile;

    public void Initialize(Actor actor) {
        this.ActorBody = actor.Body;
        this.ActorProfile = actor.Profile;
        this.Stomach = new ActorMetabolismStomach(this);
        this.RegisterEvents();
    }

    public void RegisterEvents() {
        TimeManager.MinuteChanged += (dateTime) => {
            this.UpdateAMR();
            this.UpdateBurnedCalories();
            this.Stomach.MetabolizeContent(dateTime);
        };

        TimeManager.DayChanged += (dateTime) => {
            
            this.UpdateBMR();
        };
    }

    public void UpdateBurnedCalories() {
        var bmrCalories = this.BMRPerMinute;
        var amrCalories = this.AMRPerMinute;
        this.BurnCalories(bmrCalories, amrCalories);
        
    }

    public void UpdateBodyweight(){
        var toleranceFrom = -this.CaloriesDifferenceTolerance;
        var toleranceTo = this.CaloriesDifferenceTolerance;

        if(this.CaloriesDeposit <= toleranceTo && this.CaloriesDeposit >= toleranceFrom) {
            this.MinutesInCaloriesDifference = 0;
            return;
        }

        this.MinutesInCaloriesDifference += 1;
        if(this.MinutesInCaloriesDifference < this.InfluenceBodyThreshold) {
            this.InfluenceBodyDuration = 0;
            return;
        }

        
        //var caloriesDifference = this.EnergyLevel - this.CaloriesBMR;

    }

    public void UpdateBMR() {
        var gender = this.ActorProfile.Gender;
        var age = this.ActorProfile.Age(TimeManager.Now);
        var weight = this.ActorBody.Weight;
        var height = this.ActorBody.Height;

        this.BMR = this.CalculateBMR(gender, age, height, weight);
        this.BMRPerMinute = this.BMR / 1440;
    }

    public void UpdateAMR() {
        var palFactor = 1.2f;

        this.AMR = this.CalculateAMR(this.BMR, palFactor);
        this.AMRPerMinute = this.AMR / 1440;
    }

    //@TODO: in eine Methode packen und richtig benennen wie zum Beispiel BurnCalories;

    public void BurnCalories(float bmrCalories, float activityCalories) {
        this.CaloriesDeposit -= bmrCalories;
        this.CaloriesDeposit -= activityCalories;
    }

    private float CalculateBMR(string gender, int age, int height, float weight) {
        if(gender == "Male") {
            return (float)(66 + (13.7 * weight) + (5 * height) - (6.8 * age));
        }
        else {
            return (float)(655 + (9.6 * weight) + (1.8 * height) - (4.7 * age));
        }
    }

    private float CalculateAMR(float bmr, float palFactor) {
        return bmr * (palFactor - 1);
    }
}
