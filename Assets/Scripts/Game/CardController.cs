using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerEnterHandler
{
    public CardImageController ImageController;

    public int MaxHp;
    public int Attack;
    public int Metal;
    public int Crystal;
    public int Deuterium;

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
    
    private int _id = -1;
    private bool _draggable;
    private Value<EnumType.Resource> _resource;
    private Value<EnumType.Card> _value;

    // Use this for initialization
    void Awake()
    {
        _resource=new Value<EnumType.Resource>();
        _resource[EnumType.Resource.Metal] = Metal;
        _resource[EnumType.Resource.Crystal] = Crystal;
        _resource[EnumType.Resource.Deuterium] = Deuterium;

        _value =new Value<EnumType.Card>();
        _value[EnumType.Card.MaxHp] = MaxHp;
        _value[EnumType.Card.Hp] = MaxHp;
        _value[EnumType.Card.Attack] = Attack;

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

    public void SetResource(EnumType.Resource resource, int value)
    {
        _resource[resource] = value;
    }

    public int GetResource(EnumType.Resource resource)
    {
        return _resource[resource];
    }

    public void SetStats(EnumType.Card card, int value)
    {
        _value[card] = value;
    }

    public int GetStats(EnumType.Card card)
    {
        return _value[card];
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
}
