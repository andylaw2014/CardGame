using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerEnterHandler
{
    public CardImageController ImageController;

    [HideInInspector]
    public int ID
    {
        get { return _id; }
        set
        {
            if (_id == -1 && value >= 0)
                _id = value;
        }
    }

    // Set which side of image to display
    [HideInInspector]
    public bool IsFront
    {
        get { return ImageController.IsFront; }
        set { ImageController.IsFront = value; }
    }

    // Set can it be drop
    [HideInInspector]
    public bool Draggable
    {
        get { return _draggable; }
        set
        {
            if (value && GetComponent<Draggable>() == null)
                gameObject.AddComponent<Draggable>();
            else if (!value && GetComponent<Draggable>() != null)
                Destroy(GetComponent<Draggable>());
        }
    }



    private CardStatisticController StatisticController;
    private int _id=-1;
    private bool _draggable=false;

    void Start()
    {
        StatisticController = GetComponent<CardStatisticController>();
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

    // View the card on card view
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Ensure only card face front can be viewed
        if (IsFront)
        {
            CardViewController cardViewController =
                GameObject.FindGameObjectWithTag("CardView").GetComponent<CardViewController>();
            if (cardViewController != null)
                cardViewController.SetImage(ImageController.Front);
        }
    }

    #region ManaCost
    public int Mana1Cost
    {
        get { return StatisticController.CostMana1; }
    }

    public int Mana2Cost
    {
        get { return StatisticController.CostMana2; }
    }

    public int Mana3Cost
    {
        get { return StatisticController.CostMana3; }
    }
    #endregion
}
