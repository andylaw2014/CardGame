using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// UNDONE: Clean up not necessary code.

// Allow card to be dragged.
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public bool OnPlayZone;

    private GameObject placeholder;
    // Hold the offset between mosue and object center.
    private Vector3 mouseOffSet;
    private Transform oringialParent;
    public void OnBeginDrag(PointerEventData eventData)
    {

        oringialParent = transform.parent;

        placeholder = new GameObject();
        placeholder.transform.SetParent(transform.parent);

        LayoutElement mLayoutElement = placeholder.AddComponent<LayoutElement>();
        mLayoutElement.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        mLayoutElement.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
        mLayoutElement.flexibleWidth = 0;
        mLayoutElement.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        // Calculate the mouse offset.
        mouseOffSet = transform.position.Minus(eventData.position);

        // Dragging necessary code
        transform.SetParent(transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Using mouse offset to perform a smooth drag.
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position.Add(mouseOffSet);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(oringialParent);
        transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.rotation = Quaternion.identity;

        Destroy(placeholder);

        if (OnPlayZone)
        {
            Game game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
            CardController cardController = GetComponent<CardController>();
            if (cardController == null) return;
            if(game.IsCardPlayable(cardController.ID))
                game.PlayCard(cardController.ID);
        }

    }


}
