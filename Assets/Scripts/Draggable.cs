using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// UNDONE: Clean up not necessary code.

// Allow card to be dragged.
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform returnParent = null;
    private GameObject placeholder = null;

    // Hold the offset between mosue and object center.
    private Vector3 mouseOffSet;
    private Transform oringialParent = null;
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

        returnParent = transform.parent;

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
        transform.SetParent(returnParent);
        if (oringialParent == transform.parent)
            transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.rotation = Quaternion.identity;

        Destroy(placeholder);
    }

    
}
