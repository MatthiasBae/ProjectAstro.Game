using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    //@TODO: Echtzeit einbauen
    //@TODO: StartDate festlegen damit ich die Kalorien pro Tag ausrechnen kann
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

    public static DateTime Last;
    public static DateTime Now;

    
    private void Awake() {
        TimeManager.Now = new DateTime(this.Year, this.Month, this.Day, this.Hour, this.Minute, 0);
        this.RegisterListeners();
    }

    public void RegisterListeners() {
        TickManager.OnTimeSystemTick += () => {
            TimeManager.Last = TimeManager.Now;
            TimeManager.Now = TimeManager.Now.AddMinutes(1);

            if(TimeManager.Last.Minute != TimeManager.Now.Minute) {
                MinuteChanged?.Invoke(TimeManager.Now);
            }

            if(TimeManager.Last.Hour != TimeManager.Now.Hour) {
                HourChanged?.Invoke(TimeManager.Now);
            }

            if(TimeManager.Last.Day != TimeManager.Now.Day) {
                DayChanged?.Invoke(TimeManager.Now);
            }

            if(TimeManager.Last.Month != TimeManager.Now.Month) {
                MonthChanged?.Invoke(TimeManager.Now);
            }

            if(TimeManager.Last.Year != TimeManager.Now.Year) {
                YearChanged?.Invoke(TimeManager.Now);
            }

            this.Minute = TimeManager.Now.Minute;
            this.Hour = TimeManager.Now.Hour;
            this.Day = TimeManager.Now.Day;
            this.Month = TimeManager.Now.Month;
            this.Year = TimeManager.Now.Year;
            

            TimeChanged?.Invoke(TimeManager.Now);
        };

    }

    public static TimeSpan TimePassed {
        get {
            var midnight = new DateTime(TimeManager.Now.Year, TimeManager.Now.Month, TimeManager.Now.Day, 0, 0, 0);
            return TimeManager.Now - midnight;
        }
    }

    public static TimeSpan TimeSince(DateTime from, DateTime to) {
        return to - from;
    }
}
