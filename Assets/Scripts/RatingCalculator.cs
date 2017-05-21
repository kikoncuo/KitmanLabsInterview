using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingCalculator : MonoBehaviour {

    public GameObject team;

    private float calculateAverageRating()
    {
        int possitions = team.transform.childCount;
        int sumScores = 0;
        int foundPlayers = 0;
        for (int i = 0; i < possitions-1; i++)
        {
            if (team.transform.GetChild(i).childCount > 0) { 
                sumScores = sumScores + team.transform.GetChild(i).GetChild(0).GetComponent<AthleteManager>().athleteInfo.star_rating;
                foundPlayers++;
            }
        }
        if (foundPlayers > 0)
            return (float)sumScores / foundPlayers;
        else return 0;
    }

    public void setAverageRating()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Team Rating: " + calculateAverageRating().ToString();
    }
        

}
