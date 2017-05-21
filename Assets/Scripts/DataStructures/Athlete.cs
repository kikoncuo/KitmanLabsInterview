using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Athlete {
    public Athlete()
    {
        // this is the default constructor that JSONFX needs.
    }
    public string name;
    public string avatar_url;
    public int star_rating;
    public string country;
    public string last_played;
    public string[] positions;
    public bool isInjured;
    public int id;
}
