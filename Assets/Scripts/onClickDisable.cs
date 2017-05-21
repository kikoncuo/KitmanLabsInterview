using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickDisable : MonoBehaviour {

    public void disableParent()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
