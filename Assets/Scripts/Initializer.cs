using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Loads the info of the players from a Json
public class Initializer : MonoBehaviour {

    public GameObject athlete;
    public GameObject athleteContainer;
    public TextAsset jsonFile;
    public GameObject errorPanel;
    public GameObject playerInfoPanel;
    public SetTeam setTeam;
    [HideInInspector]
    public AthleteArray athletesArray;

    private AthleteManager athleteInfoScript;


    void Start () {
        
        if (errorPanel == null) {
            Debug.LogError("ErrorPanel was not found, errors wont be shown in it (You can add it to the scene, its a prefab)");
        }
        else
        {
            errorPanel.SetActive(false);
            setAthletsOnList();
            setTeam.athleteArray = athletesArray;
        }
    }
    
	void Update () {
		
	}

    private void setAthletsOnList()
    {
        try { 
            athletesArray = JsonUtility.FromJson<AthleteArray>(jsonFile.text);
            playerInfoPanel.SetActive(true);
            for (int i = 0; i < athletesArray.athletes.Length; i++)
            {
                GameObject intstantiatedAthlete = Instantiate(athlete);
                intstantiatedAthlete.transform.SetParent(athleteContainer.transform);
                athleteInfoScript = intstantiatedAthlete.GetComponent<AthleteManager>();
                athleteInfoScript.athleteInfo = athletesArray.athletes[i];
                athleteInfoScript.setPlayerInfo();
                athleteInfoScript.playerInfoPanel = playerInfoPanel;
            }
            playerInfoPanel.SetActive(false);
        }
        catch (Exception err)
        {
            errorPanel.SetActive(true);
            errorPanel.GetComponent<ErrorManager>().setErrorText("There was a problem while reading the athlete info: "+err.Message);
        }
    }
}
