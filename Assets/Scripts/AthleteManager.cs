using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AthleteManager : MonoBehaviour {
    [HideInInspector]
    public Athlete athleteInfo;

    void Start () {
		
	}

    void Update () {
		
	}

    public void setPlayerInfo ()
    {
        StartCoroutine(setImage());
        this.gameObject.transform.GetChild(0).GetComponent<Text>().text = athleteInfo.name;
    }

    IEnumerator setImage()
    {
        WWW www = new WWW(athleteInfo.avatar_url);
        yield return www;
        this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        www.Dispose();
        www = null;
    }
}
