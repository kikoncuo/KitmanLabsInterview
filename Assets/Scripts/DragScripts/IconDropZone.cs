using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconDropZone : MonoBehaviour, IDropHandler {

    public string position;
    public GameObject errorPanel;

    public void OnDrop (PointerEventData eventData)
    {
        Sprite icon;
        DraggableListItem dL;
        DraggableIcon dI = null;
        Athlete AthleteInfo = null;
        string[] possitionsFromDraggedAthlete = null;
        bool correctPosAllowChange = false;

        //Comming from list
        if (eventData.pointerDrag.transform.childCount == 3) {
            icon = eventData.pointerDrag.transform.GetChild(1).GetComponent<Image>().sprite;
            AthleteInfo = eventData.pointerDrag.GetComponent<AthleteManager>().athleteInfo;
            possitionsFromDraggedAthlete = eventData.pointerDrag.GetComponent<AthleteManager>().athleteInfo.positions;
            for (int i = 0; i < possitionsFromDraggedAthlete.Length; i++)
            {
                if (possitionsFromDraggedAthlete[i].Equals(position))
                    correctPosAllowChange = true;
            }
            dL = eventData.pointerDrag.GetComponent<DraggableListItem>();
        }

        //Comming form field
        else
        {
            icon = eventData.pointerDrag.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
            dL = eventData.pointerDrag.transform.GetChild(0).GetComponent<DraggableListItem>();
            possitionsFromDraggedAthlete = eventData.pointerDrag.transform.GetChild(0).GetComponent<AthleteManager>().athleteInfo.positions;
            AthleteInfo = eventData.pointerDrag.transform.GetChild(0).GetComponent<AthleteManager>().athleteInfo;
            for (int i = 0; i < possitionsFromDraggedAthlete.Length; i++)
            {
                if (possitionsFromDraggedAthlete[i].Equals(position))
                    correctPosAllowChange = true;
            }
            if (AthleteInfo.isInjured)
                    correctPosAllowChange = false;
            dI = eventData.pointerDrag.GetComponent<DraggableIcon>();
            if (correctPosAllowChange)
                eventData.pointerDrag.transform.GetChild(0).transform.SetParent(this.transform);
        }

        if (correctPosAllowChange) {
            //If it has a draggableIconComponent change the icon of the original object for the dragged one
            if (dI != null)
            {
                dI.spriteToReset = this.gameObject.transform.GetComponent<Image>().sprite;
                if (eventData.pointerDrag == this.gameObject)
                    dI.spriteToReset = icon;
                //If the object has children also try to move it
                if (this.transform.childCount>1) {
                    this.transform.GetChild(0).SetParent(eventData.pointerDrag.transform);
                }
            }
            else
            {
                if (this.transform.childCount > 0)
                {
                    GameObject temp = this.transform.GetChild(0).gameObject;
                    temp.transform.SetParent(dL.parentToReturn);
                    temp.transform.SetSiblingIndex(0);
                }
            }
            if (dL != null)
            {
                //And move athlete to the new position
                dL.parentToReturn = this.transform;
                this.gameObject.transform.GetComponent<Image>().sprite = icon;
            }
            correctPosAllowChange = false;
        }
        //Show why it cant be placed
        else
        {
            errorPanel.SetActive(true);
            if(!AthleteInfo.isInjured)
                errorPanel.GetComponent<ErrorManager>().setErrorText(AthleteInfo.name+" can only play in " + string.Join(" and ", possitionsFromDraggedAthlete) + ", this possition is " + position);
            else
                errorPanel.GetComponent<ErrorManager>().setErrorText(AthleteInfo.name + " is injured and can't play");
        }
    }
}
