using UnityEngine;
using UnityEngine.UI;

// Control card image between front image and card back. 
public class CardImageController : MonoBehaviour
{
    
    [HideInInspector]
    public Sprite Front; // Front image of card.
    [HideInInspector]
    public Sprite Back;  // Card back of card.
    
    // Initial front image and card back.
    void Start()
    {
        Front = GetComponent<Image>().sprite;
        Back = GameObject.FindGameObjectWithTag("CardBack").GetComponent<CardBack>().sprite;
    }

    // Set card image to front image.
    public void SetFront()
    {
        GetComponent<Image>().sprite = Front;
    }

    // Set card image to card back.
    public void SetBack()
    {
        GetComponent<Image>().sprite = Back;
    }
}
