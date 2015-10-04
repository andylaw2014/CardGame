using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerEnterHandler
{
    public CardImageController ImageController;

    [HideInInspector]
    public int Id
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
            _draggable = value;
            if (_draggable && GetComponent<Draggable>() == null)
                gameObject.AddComponent<Draggable>();
            else if (!_draggable && GetComponent<Draggable>() != null)
                Destroy(GetComponent<Draggable>());
        }
    }
    
    private CardStatisticController _statisticController;
    private int _id=-1;
    private bool _draggable;

    void Start()
    {
        _statisticController = GetComponent<CardStatisticController>();
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
        if (!IsFront) return;
        var cardViewController =
            GameObject.FindGameObjectWithTag("CardView").GetComponent<CardViewController>();
        if (cardViewController != null)
            cardViewController.SetImage(ImageController.Front);
    }

    #region ManaCost
    public int Mana1Cost
    {
        get { return _statisticController.CostMana1; }
    }

    public int Mana2Cost
    {
        get { return _statisticController.CostMana2; }
    }

    public int Mana3Cost
    {
        get { return _statisticController.CostMana3; }
    }
    #endregion
}
