using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AthleteManager : MonoBehaviour {
    [HideInInspector]
    public Athlete athleteInfo;
    [HideInInspector]
    public GameObject playerInfoPanel;

    public void setPlayerInfo ()
    {
        StartCoroutine(setImage());
        this.gameObject.transform.GetChild(0).GetComponent<Text>().text = athleteInfo.name;
        this.gameObject.transform.GetChild(2).GetComponent<Text>().text = string.Join(" and ", athleteInfo.positions);
    }

    IEnumerator setImage()
    {
        WWW www = new WWW(athleteInfo.avatar_url);
        yield return www;
        this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        www.Dispose();
        www = null;
    }

    public void showPlayerInfo()
    {
        playerInfoPanel.SetActive(true);
        playerInfoPanel.transform.GetChild(0).GetComponent<Text>().text = athleteInfo.name;
        playerInfoPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Star rating: "+athleteInfo.star_rating.ToString();
        playerInfoPanel.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Country: " + athleteInfo.country;
        playerInfoPanel.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "Last match: " + athleteInfo.last_played;
        playerInfoPanel.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "Possitions:  " + string.Join(" and ", athleteInfo.positions);
        playerInfoPanel.transform.GetChild(1).GetChild(4).GetComponent<Text>().text = "Is injured: " + athleteInfo.isInjured.ToString();
        playerInfoPanel.transform.GetChild(2).GetComponent<Image>().sprite = this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite;
    }
}
