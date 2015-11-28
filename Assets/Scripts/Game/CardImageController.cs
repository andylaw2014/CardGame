using UnityEngine;
using UnityEngine.UI;

// Control card image between front image and card back. 
public class CardImageController : MonoBehaviour
{
    private bool _isFront;
    public Sprite Front; // Front image of card.

    [HideInInspector]
    public bool IsFront
    {
        get { return _isFront; }

        set
        {
            _isFront = value;
            var back = GameController2.Instance.GuiController.CardBack;
            // Set card image to front or back image.
            GetComponent<Image>().sprite = _isFront ? Front : back;
        }
    }
}