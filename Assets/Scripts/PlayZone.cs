using UnityEngine;
using UnityEngine.EventSystems;

public class PlayZone : MonoBehaviour, IDropHandler
{
    
    public void OnDrop(PointerEventData eventData)
    {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggable != null)
            draggable.OnPlayZone = true;
    }

}
