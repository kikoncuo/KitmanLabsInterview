using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Loads the info of the players from a Json
public class Initializer : MonoBehaviour {

    public GameObject athlete;
    public GameObject athleteContainer;
    public TextAsset jsonFile;

    void Start () {
        AthleteArray athletesArray = JsonUtility.FromJson<AthleteArray>(jsonFile.text);
        for (int i = 0; i < athletesArray.athletes.Length; i++)
        {
            GameObject intstantiatedAthlete = Instantiate(athlete);
            intstantiatedAthlete.transform.SetParent(athleteContainer.transform);
            AthleteInfo athleteInfoScript = intstantiatedAthlete.GetComponent<AthleteInfo>();
            athleteInfoScript.athleteInfo = athletesArray.athletes[i];
            athleteInfoScript.setPlayerInfo();
        }
	}
	
	void Update () {
		
	}
}
