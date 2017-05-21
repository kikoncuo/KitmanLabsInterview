using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableIcon : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform parentToReturn;
    [HideInInspector]
    public Sprite spriteToReset;
    public GameObject iconPrefab;

    private GameObject playerIcon;
    private bool containsAthlete = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (hasChild()) {
            parentToReturn = this.transform.parent;
            instantiateIcon();
            spriteToReset = playerIcon.GetComponent<Image>().sprite;
            this.transform.GetComponent<Image>().sprite = spriteToReset;
            containsAthlete = true;
        }else
        {
            containsAthlete = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (containsAthlete)
        {
            playerIcon.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (containsAthlete)
        {
            playerIcon.GetComponent<CanvasGroup>().blocksRaycasts = true;
            this.transform.SetParent(parentToReturn);
            this.transform.GetComponent<Image>().sprite = spriteToReset;
            Destroy(playerIcon);
        }
    }

    private void instantiateIcon()
    {
        playerIcon = Instantiate(iconPrefab);
        playerIcon.transform.SetParent(parentToReturn);
        playerIcon.GetComponent<Image>().sprite =
            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
        playerIcon.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    private bool hasChild() {
        if (this.gameObject.transform.childCount > 0)
        {
            return true;
        }
        return false;
    }
}
