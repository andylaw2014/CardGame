using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Allow card to be dragged.
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public bool OnPlayZone;

    private GameObject _placeHolder;
    private Vector3 _mouseOffSet; // Hold the offset between mosue and object center.
    private Transform _parent;
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnPlayZone = false;
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
        transform.SetParent(_parent);
        transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.rotation = Quaternion.identity;

        Destroy(_placeHolder);

        if (!OnPlayZone) return;
        var game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        var cardController = GetComponent<CardController>();
        if (cardController == null) return;
        if(game.IsCardPlayable(cardController.Id))
            game.PlayCard(cardController.Id);
    }


}
