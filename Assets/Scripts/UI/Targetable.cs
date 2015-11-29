using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class Targetable : MonoBehaviour, IDropHandler
    {
        public enum Type
        {
            Card,
            Zone
        }

        public Type Source;

        public void OnDrop(PointerEventData eventData)
        {
            var drag = eventData.pointerDrag.GetComponent<Draggable>();
            if (drag != null)
                drag.Destination = this;
        }
    }
}