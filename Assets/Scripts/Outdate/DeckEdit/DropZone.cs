using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Outdate.DeckEdit
{
    public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            var d = eventData.pointerDrag.GetComponent<Draggable1>();
            if (d != null)
            {
                d.parentToReturnTo = transform;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var d = eventData.pointerDrag.GetComponent<Draggable1>();
            if (d != null)
            {
                d.placeholderParent = transform;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var d = eventData.pointerDrag.GetComponent<Draggable1>();
            if (d != null && d.placeholderParent == transform)
            {
                d.placeholderParent = d.parentToReturnTo;
            }
        }
    }
}