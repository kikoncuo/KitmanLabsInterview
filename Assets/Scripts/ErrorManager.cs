using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setErrorText(string messageString, string titleString = "ERROR")
    {
        this.gameObject.transform.GetChild(0).GetComponent<Text>().text = titleString;
        this.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = messageString;
    }

}
