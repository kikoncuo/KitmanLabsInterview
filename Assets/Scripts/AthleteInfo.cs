using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AthleteInfo : MonoBehaviour {
    [HideInInspector]
    public Athlete athleteInfo;

    void Start () {
		
	}

    void Update () {
		
	}

    public void setPlayerInfo ()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Text>().text = athleteInfo.name;
    }
}
