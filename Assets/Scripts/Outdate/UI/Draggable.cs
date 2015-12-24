using Assets.Scripts.Outdate.Core;
using Assets.Scripts.Outdate.Infrastructure;
using Assets.Scripts.Outdate.UI.Command;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Allow card to be dragged.

namespace Assets.Scripts.Outdate.UI
{
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _mouseOffSet; // Hold the offset between mosue and object center.
        private Transform _parent;
        private GameObject _placeHolder;
        public Targetable Destination;

        public void OnBeginDrag(PointerEventData eventData)
        {
            Destination = null;
            _parent = transform.parent;

            _placeHolder = new GameObject();
            _placeHolder.transform.SetParent(transform.parent);

            var layoutElement = _placeHolder.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
            layoutElement.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
            layoutElement.flexibleWidth = 0;
            layoutElement.flexibleHeight = 0;

            _placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());

            // Calculate the mouse offset.
            _mouseOffSet = transform.position.Minus(eventData.position);

            // Dragging necessary code
            transform.SetParent(transform.parent.parent);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        // Using mouse offset to perform a smooth drag.
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position.Add(_mouseOffSet);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var handle = false;
            if (Destination != null)
                handle = GameObject.FindGameObjectWithTag("GameController")
                    .GetComponent<GameController>().Game.Handle(new DragCommand(this, Destination));
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (!handle)
            {
                transform.SetParent(_parent);
                transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                transform.rotation = Quaternion.identity;
            }
            Destroy(_placeHolder);
        }
    }
}