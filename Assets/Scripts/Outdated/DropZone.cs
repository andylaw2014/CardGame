using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().state == GameController.GameState.PlayerMain)
        {
            Draggable mDraggable = eventData.pointerDrag.GetComponent<Draggable>();
            Card card = eventData.pointerDrag.GetComponent<Card>();
            if (mDraggable != null && card != null)
            {
                if (card.IsPlayable())
                    card.OnPlay(transform);
            }
        }
    }
}
