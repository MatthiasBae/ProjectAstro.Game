using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActorProfile {
    public string FirstName;
    public string LastName;
    public string Gender;
    
    public int BirthdayDay;
    public int BirthdayMonth;
    public int BirthdayYear;
    
    public DateTime Birthday {
        get => new DateTime(this.BirthdayYear, this.BirthdayMonth, this.BirthdayDay);
    }
    public int Age(DateTime now) {
        var birthday = this.Birthday;
        int age = now.Year - birthday.Year;

        if(now.Month < birthday.Month || (now.Month == birthday.Month && now.Day < birthday.Day))
            age--;

        return age;
    }
}
