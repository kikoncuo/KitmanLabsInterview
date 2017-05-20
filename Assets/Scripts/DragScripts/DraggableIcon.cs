using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableIcon : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform parentToReturn;
    public GameObject playerIcon;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentToReturn = this.transform.parent;
        instantiateIcon();

    }

    public void OnDrag(PointerEventData eventData)
    {
        playerIcon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        playerIcon.GetComponent<CanvasGroup>().blocksRaycasts = true;
        playerIcon.SetActive(false);
        /*this.gameObject.SetActive(true);
        this.transform.SetParent(parentToReturn);
        this.transform.SetSiblingIndex(childPosition);*/
    }

    private void instantiateIcon()
    {
        //playerIcon.SetActive(true);
        playerIcon = Instantiate(playerIcon);
        playerIcon.transform.SetParent(parentToReturn);
        playerIcon.GetComponent<Image>().sprite =
            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
        playerIcon.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
