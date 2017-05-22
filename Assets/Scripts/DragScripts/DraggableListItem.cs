using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableListItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //private Vector2 originalPosition;
    [HideInInspector]
    public Transform parentToReturn;
    public GameObject iconPrefab;


    private RatingCalculator rCalculator1;
    private RatingCalculator rCalculator2;
    private int childPosition;
    private GameObject playerIcon;

    public void Start()
    {
        rCalculator1 = GameObject.FindWithTag("AverageStars1").GetComponent<RatingCalculator>();
        rCalculator2 = GameObject.FindWithTag("AverageStars2").GetComponent<RatingCalculator>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        //originalPosition = this.transform.position;
        childPosition = this.transform.GetSiblingIndex();
        parentToReturn = this.transform.parent;
        instantiateIcon();
        this.transform.SetParent(this.transform.parent.parent.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        playerIcon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //playerIcon.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //playerIcon.SetActive(false);
        this.gameObject.SetActive(true);
        this.transform.SetParent(parentToReturn);
        this.transform.SetSiblingIndex(childPosition);
        Destroy(playerIcon);
        rCalculator1.setAverageRating();
        rCalculator2.setAverageRating();
    }

    private void instantiateIcon()
    {
        //playerIcon.SetActive(true);
        playerIcon = Instantiate(iconPrefab);
        playerIcon.transform.SetParent(parentToReturn);
        playerIcon.GetComponent<Image>().sprite = 
            this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite;
        playerIcon.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

}
