using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    //@TODO: Echtzeit einbauen
    
    public static event Action<DateTime> TimeChanged;
    
    public static event Action<DateTime> MinuteChanged;
    public static event Action<DateTime> HourChanged;
    public static event Action<DateTime> DayChanged;
    public static event Action<DateTime> MonthChanged;
    public static event Action<DateTime> YearChanged;

    public int Day;
    public int Month;
    public int Year;
    public int Hour;
    public int Minute;

    private DateTime PreviousDate;
    private DateTime ActualDate;

    private void Awake() {
        this.ActualDate = new DateTime(this.Year, this.Month, this.Day, this.Hour, this.Minute, 0);
        this.RegisterListeners();
    }

    public void RegisterListeners() {
        TickManager.OnTimeSystemTick += () => {
            this.PreviousDate = this.ActualDate;
            this.ActualDate = this.ActualDate.AddMinutes(1);

            if(this.PreviousDate.Minute != this.ActualDate.Minute) {
                MinuteChanged?.Invoke(this.ActualDate);
            }

            if(this.PreviousDate.Hour != this.ActualDate.Hour) {
                HourChanged?.Invoke(this.ActualDate);
            }

            if(this.PreviousDate.Day != this.ActualDate.Day) {
                DayChanged?.Invoke(this.ActualDate);
            }

            if(this.PreviousDate.Month != this.ActualDate.Month) {
                MonthChanged?.Invoke(this.ActualDate);
            }

            if(this.PreviousDate.Year != this.ActualDate.Year) {
                YearChanged?.Invoke(this.ActualDate);
            }

            this.Minute = this.ActualDate.Minute;
            this.Hour = this.ActualDate.Hour;
            this.Day = this.ActualDate.Day;
            this.Month = this.ActualDate.Month;
            this.Year = this.ActualDate.Year;
            

            TimeChanged?.Invoke(this.ActualDate);
        };
    }
}
