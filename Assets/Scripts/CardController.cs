using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerEnterHandler
{
    [HideInInspector]
    public int ID { get; private set; }

    public CardImageController ImageController;

    void Start()
    {
        ID = -1;
    }

    public void SetDraggable(bool draggable)
    {
        if (draggable && GetComponent<Draggable>() == null)
            gameObject.AddComponent<Draggable>();

        if (!draggable && GetComponent<Draggable>() != null)
            Destroy(GetComponent<Draggable>());
    }

    // Set which side of image to display
    public void SetImageSide(bool isFront)
    {
        ImageController.IsFront = isFront;
    }

    public int SetID(int id = -1)
    {
        if (ID == -1)
            ID = id;
        return ID;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
    }

    public virtual bool IsPlayable()
    {
        return true;
    }

    public void Play()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ImageController.IsFront)
        {
            CardViewController cardViewController = GameObject.FindGameObjectWithTag("CardView").GetComponent<CardViewController>();
            if(cardViewController!=null)
                cardViewController.SetImage(ImageController.Front);
        }
    }

}
