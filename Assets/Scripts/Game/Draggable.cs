using Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Allow card to be dragged.
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject _placeHolder;
    private Vector3 _mouseOffSet; // Hold the offset between mosue and object center.
    private Transform _parent;
    private bool _dragEnd;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragEnd = false;
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
        if (_dragEnd) return;
        EndDrag();
    }

    public void EndDrag()
    {
        _dragEnd = true;
        transform.SetParent(_parent);
        transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.rotation = Quaternion.identity;
        Destroy(_placeHolder);
    }


}
