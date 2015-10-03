using UnityEngine;
using UnityEngine.UI;

// Control card image between front image and card back. 
public class CardImageController : MonoBehaviour
{
    public Sprite Front; // Front image of card.
    public CardBack Back;  // Card back of card.

    [HideInInspector]
    public bool IsFront
    {
        get
        {
            return _isFront;
        }

        set
        {
            _isFront = value;
            // Set card image to front or back image.
            GetComponent<Image>().sprite = _isFront ? Front : Back.sprite;
        }
    }

    private bool _isFront;
}
