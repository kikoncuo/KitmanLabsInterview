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
    public SetTeam setRandomTeam;
    public SetTeam setBestTeam;
    [HideInInspector]
    public AthleteArray athletesArray;

    private AthleteManager athleteInfoScript;
    private string internetJson;


    void Start () {
        
        if (errorPanel == null) {
            Debug.LogError("ErrorPanel was not found, errors wont be shown in it (You can add it to the scene, its a prefab)");
        }
        else
        {
            errorPanel.SetActive(false);
            setAthletsOnList();
            //TODO get this data the other way around
            setRandomTeam.athleteArray = athletesArray;
            setBestTeam.athleteArray = athletesArray;
        }
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
            playerInfoPanel.SetActive(false);
            errorPanel.SetActive(true);
            errorPanel.GetComponent<ErrorManager>().setErrorText("There was a problem while reading the athlete info: "+err.Message);
        }
    }

    public void setAthletsOnListFromInternet(string url)
    {
        StartCoroutine(getJson(url));
    }

    IEnumerator getJson(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        internetJson = www.text;
        www.Dispose();
        www = null;
        try
        {
            athletesArray = JsonUtility.FromJson<AthleteArray>(internetJson);
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
            playerInfoPanel.SetActive(false);
            errorPanel.SetActive(true);
            errorPanel.GetComponent<ErrorManager>().setErrorText("There was a problem while reading the athlete info: " + err.Message + ". This may be because the url isn't correct");
        }
    }
}
