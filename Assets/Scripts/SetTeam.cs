using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTeam : MonoBehaviour {
    
    public GameObject team1;
    public GameObject team2;
    public GameObject playerContainer;
    [HideInInspector]
    public AthleteArray athleteArray;
    public RatingCalculator ratingCalculator1;
    public RatingCalculator ratingCalculator2;

    private List<int> fullBack = new List<int>();
    private List<int> centre = new List<int>();
    private List<int> wing = new List<int>();
    private List<int> outHalf = new List<int>();
    private List<int> alock = new List<int>();
    private List<int> flanker = new List<int>();
    private List<int> hooker = new List<int>();
    private List<int> nEight = new List<int>();
    private List<int> scrumHalf = new List<int>();
    private List<int> prop = new List<int>();

    

    private void divideByPossitions()
    {
        for (int i = 0; i < athleteArray.athletes.Length; i++)
        {
            for (int j = 0; j < athleteArray.athletes[i].positions.Length; j++)
            {
                if(athleteArray.athletes[i].positions[j].Equals("full-back"))
                {
                    fullBack.Add(athleteArray.athletes[i].id);
                }
                else if (athleteArray.athletes[i].positions[j].Equals("centre"))
                {
                    centre.Add(athleteArray.athletes[i].id);
                }
                else if (athleteArray.athletes[i].positions[j].Equals("winger"))
                {
                    wing.Add(athleteArray.athletes[i].id);
                }
                else if (athleteArray.athletes[i].positions[j].Equals("out-half"))
                {
                    outHalf.Add(athleteArray.athletes[i].id);
                }
                else if (athleteArray.athletes[i].positions[j].Equals("lock"))
                {
                    alock.Add(athleteArray.athletes[i].id);
                }
                else if (athleteArray.athletes[i].positions[j].Equals("flanker"))
                {
                    flanker.Add(athleteArray.athletes[i].id);
                }
                else if (athleteArray.athletes[i].positions[j].Equals("hooker"))
                {
                    hooker.Add(athleteArray.athletes[i].id);
                }
                else if (athleteArray.athletes[i].positions[j].Equals("number-eight"))
                {
                    nEight.Add(athleteArray.athletes[i].id);
                }
                else if (athleteArray.athletes[i].positions[j].Equals("scrum-half"))
                {
                    scrumHalf.Add(athleteArray.athletes[i].id);
                }
                else if (athleteArray.athletes[i].positions[j].Equals("prop"))
                {
                    prop.Add(athleteArray.athletes[i].id);
                }

            }
        }
    }

    private Transform returnRandomPlayerInList(List<int> compatiblePlayers)
    {
        if (compatiblePlayers.Count > 0)
        {
            Transform compatiblePlayer = null;
            int randomTries = 0;
            while (randomTries<1000)
            {
                int compatiblePlayerPos = compatiblePlayers.ElementAt(Random.Range(0, compatiblePlayers.Count));
                for (int j = 0; j < playerContainer.transform.childCount; j++)
                {
                    int playeriD = playerContainer.transform.GetChild(j).GetComponent<AthleteManager>().athleteInfo.id;
                    if (playeriD == compatiblePlayerPos && !playerContainer.transform.GetChild(j).GetComponent<AthleteManager>().athleteInfo.isInjured)
                    {
                        compatiblePlayer = playerContainer.transform.GetChild(j);
                        j = playerContainer.transform.childCount;
                    }
                }
                randomTries++;
            }
            //if (compatiblePlayer == null) ;
                //DO THIS IN ERROR WINDOW Debug.Log("There are not enough players to fill all spaces TODO: do this in the error window");
            return compatiblePlayer;
        }
        else
            return null;
    }

    //TODO: This only returns the best one aviable, but there may be better combinations, this should be fixed
    private Transform returnBestPlayerInList(List<int> compatiblePlayers)
    {
        if (compatiblePlayers.Count > 0)
        {
            Transform bestPlayer = null;
            int bestScore = 0;
            for (int i = 0; i < compatiblePlayers.Count; i++)
            {
                int compatiblePlayerPos = compatiblePlayers.ElementAt(i);
                for (int j = 0; j < playerContainer.transform.childCount; j++)
                {
                    int playeriD = playerContainer.transform.GetChild(j).GetComponent<AthleteManager>().athleteInfo.id;
                    if (playeriD == compatiblePlayerPos)
                    {
                        if (playerContainer.transform.GetChild(j).GetComponent<AthleteManager>().athleteInfo.star_rating > bestScore && !playerContainer.transform.GetChild(j).GetComponent<AthleteManager>().athleteInfo.isInjured) { 
                            bestPlayer = playerContainer.transform.GetChild(j);
                            bestScore = playerContainer.transform.GetChild(j).GetComponent<AthleteManager>().athleteInfo.star_rating;
                        }
                        j = playerContainer.transform.childCount;
                    }
                }
            }
            //if (bestPlayer == null) ;
                //DO THIS IN ERROR WINDOWDebug.Log("There are not enough players with score to fill all spaces TODO: do this in the error window");
            return bestPlayer;
        }
        else
            return null;
    }

    private void clearField(GameObject team)
    {
        for (int i = 0; i < team.transform.childCount; i++)
        {
            if (team.transform.GetChild(i).childCount>0)
            {
                team.transform.GetChild(i).GetChild(0).SetParent(playerContainer.transform);
            }
        }
    }

    public void setRandom()
    {
        divideByPossitions();
        clearField(team1);
        clearField(team2);
        for (int i = 0; i < team1.transform.childCount; i++)
        {
            if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("full-back"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(fullBack);
                if (compatiblePlayer != null) { 
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("centre"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(centre);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("winger"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(wing);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("out-half"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(outHalf);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("lock"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(alock);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("flanker"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(flanker);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("hooker"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(hooker);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("number-eight"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(nEight);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("scrum-half"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(scrumHalf);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("prop"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(prop);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
        }
        ratingCalculator1.setAverageRating();
        for (int i = 0; i < team2.transform.childCount; i++)
        {
            if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("full-back"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(fullBack);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("centre"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(centre);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("winger"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(wing);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("out-half"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(outHalf);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("lock"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(alock);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("flanker"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(flanker);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("hooker"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(hooker);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("number-eight"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(nEight);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("scrum-half"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(scrumHalf);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("prop"))
            {
                Transform compatiblePlayer = returnRandomPlayerInList(prop);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
        }
        ratingCalculator2.setAverageRating();
    }

    public void setBest()
    {
        divideByPossitions();
        clearField(team1);
        clearField(team2);
        for (int i = 0; i < team1.transform.childCount; i++)
        {
            if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("full-back"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(fullBack);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("centre"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(centre);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("winger"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(wing);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("out-half"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(outHalf);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("lock"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(alock);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("flanker"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(flanker);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("hooker"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(hooker);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("number-eight"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(nEight);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("scrum-half"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(scrumHalf);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
            else if (team1.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("prop"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(prop);
                if (compatiblePlayer != null)
                {
                    team1.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team1.transform.GetChild(i));
                }
            }
        }
        ratingCalculator1.setAverageRating();
        for (int i = 0; i < team2.transform.childCount; i++)
        {
            if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("full-back"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(fullBack);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("centre"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(centre);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("winger"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(wing);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("out-half"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(outHalf);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("lock"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(alock);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("flanker"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(flanker);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("hooker"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(hooker);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("number-eight"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(nEight);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("scrum-half"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(scrumHalf);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
            else if (team2.transform.GetChild(i).GetComponent<IconDropZone>().position.Equals("prop"))
            {
                Transform compatiblePlayer = returnBestPlayerInList(prop);
                if (compatiblePlayer != null)
                {
                    team2.transform.GetChild(i).GetComponent<Image>().sprite = compatiblePlayer.GetChild(1).GetComponent<Image>().sprite;
                    compatiblePlayer.SetParent(team2.transform.GetChild(i));
                }
            }
        }
        ratingCalculator2.setAverageRating();
    }

}
