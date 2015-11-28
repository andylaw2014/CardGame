using UnityEngine;
using UnityEngine.EventSystems;

public class DragZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var afterDrag = eventData.pointerDrag.GetComponent<AfterDrag>();
        if (afterDrag == null) return;
        var drag = eventData.pointerDrag.GetComponent<Draggable>();
        if (drag != null)
            drag.EndDrag();
        afterDrag.Execute();
    }
}