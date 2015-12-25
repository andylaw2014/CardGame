
using Assets.Scripts.Outdate.Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _mouseOffSet; // Hold the offset between mosue and object center.
        private Transform _parent;
        private GameObject _placeHolder;

        public void OnBeginDrag(PointerEventData eventData)
        {
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
            GetComponent<CanvasGroup>().blocksRaycasts = true;
                transform.SetParent(_parent);
                transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                transform.rotation = Quaternion.identity;
            Destroy(_placeHolder);
        }
    }
}
