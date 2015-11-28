using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour, IPointerClickHandler
{
    private bool _selected;

    public bool IsSelected
    {
        get { return _selected; }
        set
        {
            var cardController = GetComponent<CardController>();
            _selected = value;

            // UNDONE: Better Setter
            cardController.ToggleSelectedGlow(_selected);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        IsSelected = !IsSelected;
    }
}