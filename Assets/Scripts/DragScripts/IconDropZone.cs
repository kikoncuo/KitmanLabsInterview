using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconDropZone : MonoBehaviour, IDropHandler {

    public void OnDrop (PointerEventData eventData)
    {
        Sprite icon;
        //Debug.Log(eventData.pointerDrag.name +" dropped in " + this.gameObject.name);
        if (eventData.pointerDrag.transform.childCount == 2) {
            icon = eventData.pointerDrag.transform.GetChild(1).GetComponent<Image>().sprite;
        }
        else
        {
            icon = eventData.pointerDrag.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
        }
        this.gameObject.transform.GetComponent<Image>().sprite = icon;
        DraggableListItem d = eventData.pointerDrag.GetComponent<DraggableListItem>();
        if (d != null){
            d.parentToReturn = this.transform;
        }
    }
}
