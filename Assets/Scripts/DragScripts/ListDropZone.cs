using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ListDropZone : MonoBehaviour, IDropHandler{
    private RatingCalculator rCalculator1;
    private RatingCalculator rCalculator2;

    public void Start()
    {
        rCalculator1 = GameObject.FindWithTag("AverageStars1").GetComponent<RatingCalculator>();
        rCalculator2 = GameObject.FindWithTag("AverageStars2").GetComponent<RatingCalculator>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Transform athlete = null;
        DraggableIcon dI = null;
        if (eventData.pointerDrag.transform.childCount == 1)
        {
            athlete = eventData.pointerDrag.transform.GetChild(0);
            dI = eventData.pointerDrag.GetComponent<DraggableIcon>();
        }
        if (athlete != null)
        {
            dI.spriteToReset = dI.blankSprite;
            athlete.SetParent(this.transform);
            athlete.SetSiblingIndex(0);
        }
        rCalculator1.setAverageRating();
        rCalculator2.setAverageRating();
    }
}
