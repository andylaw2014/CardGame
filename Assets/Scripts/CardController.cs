using UnityEngine;

public class CardController : MonoBehaviour
{
    private CardImageController ImageController;
    void Start()
    {
        ImageController = GetComponent<CardImageController>();
    }

    public void SetDraggable(bool draggable)
    {
        if (draggable && GetComponent<Draggable>() == null)
            gameObject.AddComponent<Draggable>();

        if (!draggable && GetComponent<Draggable>() != null)
            Destroy(GetComponent<Draggable>());
    }
    
}
