using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Loads the info of the players from a Json
public class Initializer : MonoBehaviour {

    public TextAsset jsonFile;

    // Use this for initialization
    void Start () {
        AthleteArray athletes = JsonUtility.FromJson<AthleteArray>(jsonFile.text);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
