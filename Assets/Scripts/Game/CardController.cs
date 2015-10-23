using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IPointerEnterHandler
{
    public CardImageController ImageController;

    public int MaxHp;
    public int Attack;
    public int Metal;
    public int Crystal;
    public int Deuterium;

    private readonly Color _orignalColor = Color.white;
    private readonly Color _playableColor = new Color(153/255f,1,153/255f,1);
    private readonly Color _selectableColor = new Color(153 / 255f, 1, 153 / 255f, 1);
    private readonly Color _selectedColor = new Color(1, 1, 153 / 255f, 1);
    private readonly Color _attackColor = new Color(1, 153 / 255f, 153 / 255f, 1);

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
    public bool AllowToPlay
    {
        get { return _allowToPlay; }
        set
        {
            _allowToPlay = value;
            if (_allowToPlay && GetComponent<PlayCard>() == null)
                gameObject.AddComponent<PlayCard>();
            else if (!_allowToPlay && GetComponent<PlayCard>() != null)
                Destroy(GetComponent<PlayCard>());
        }
    }
    
    [HideInInspector]
    public bool Selectable
    {
        get { return _selectable; }
        set
        {
            _selectable = value;
            if (_selectable && GetComponent<Selectable>() == null)
                gameObject.AddComponent<Selectable>();
            else if (!_selectable && GetComponent<Selectable>() != null)
                Destroy(GetComponent<Selectable>());
        }
    }

    private int _id = -1;
    private bool _allowToPlay;
    private bool _selectable;
    private Value<EnumType.Resource> _resource;
    private Value<EnumType.Card> _value;

    // Use this for initialization
    void Awake()
    {
        _resource = new Value<EnumType.Resource>();
        _resource[EnumType.Resource.Metal] = Metal;
        _resource[EnumType.Resource.Crystal] = Crystal;
        _resource[EnumType.Resource.Deuterium] = Deuterium;

        _value = new Value<EnumType.Card>();
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

    public virtual bool IsAttackable()
    {
        return true;
    }

    public virtual bool IsDefencable()
    {
        return true;
    }

    public void Play()
    {

    }

    public bool IsSelected()
    {
        return GetComponent<Selectable>().IsSelected;
    }

    #region Stats
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
    #endregion

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

    public void TogglePlayableGlow(bool turnOn)
    {
        GetComponent<Image>().color = turnOn ? _playableColor : _orignalColor;
    }

    public void ToggleSelectableGlow(bool turnOn)
    {
        GetComponent<Image>().color = turnOn ? _selectableColor : _orignalColor;
    }

    public void ToggleSelectedGlow(bool turnOn)
    {
        GetComponent<Image>().color = turnOn ? _selectedColor : _selectableColor;
    }

    public void ToggleAttackGlow(bool turnOn)
    {
        GetComponent<Image>().color = turnOn ? _attackColor : _selectableColor;
    }
}
