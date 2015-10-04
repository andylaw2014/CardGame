using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private GameObject _handZone;
    private GameObject _minionZone;
    private Dictionary<int, CardController> _hand;
    private PlayerStatisticController _stats;
    private int _maxId;
    private const int MaxMana = 10;
    void Start()
    {
        _handZone = gameObject.FindChildWithTag("HandZone");
        _minionZone = gameObject.FindChildWithTag("MinionZone");
        _stats = gameObject.FindComponentInChildWithTag<PlayerStatisticController>("Stat");
        _hand = new Dictionary<int, CardController>();
        _maxId = -1;
    }

    public int DrawCard(string cardName, int id = -1, bool isPlayer = true)
    {
        var card = Instantiate(Resources.Load(cardName)) as GameObject;
        var cardController = card.GetComponent<CardController>();

        cardController.IsFront = isPlayer;
        cardController.Draggable = isPlayer;

        int mid = id == -1 ? GetID() : id;
        cardController.Id = mid;
        _hand.Add(mid, cardController);
        cardController.SetParent(_handZone.transform);
        return id;
    }

    #region PlayCard
    // Is a card playable
    public bool IsPlayable(int id)
    {
        var cardToPlayController = FindCardControllerByID(id);
        bool enoughMana = (Mana1 >= cardToPlayController.Mana1Cost) && (Mana2 >= cardToPlayController.Mana2Cost) &&
                          (Mana3 >= cardToPlayController.Mana3Cost);
        return cardToPlayController.IsPlayable() && enoughMana;
    }

    public void Play(int id)
    {
        var cardToPlayController = FindCardControllerByID(id);
        Mana1 -= cardToPlayController.Mana1Cost;
        Mana2 -= cardToPlayController.Mana2Cost;
        Mana3 -= cardToPlayController.Mana3Cost;

        // UNDONE: Event Card
        var cardController = FindCardControllerByID(id);
        cardController.Draggable = false;
        cardController.SetParent(_minionZone.transform);
        cardController.IsFront = true;
        cardController.Play();
    }
    #endregion

    #region Mana
    public void ResetMana()
    {
        _stats.Mana1 = _stats.MaxMana1;
        _stats.Mana2 = _stats.MaxMana2;
        _stats.Mana3 = _stats.MaxMana3;
    }

    public bool IsMana1Full
    {
        get { return Mana1 >= MaxMana; }
    }


    public bool IsMana2Full
    {
        get { return Mana2 >= MaxMana; }
    }


    public bool IsMana3Full
    {
        get { return Mana3 >= MaxMana; }
    }

    public bool IsManaFull
    {
        get { return IsMana1Full && IsMana2Full && IsMana3Full; }
    }

    public void AddMana1()
    {
        MaxMana1++;
    }

    public void AddMana2()
    {
        MaxMana2++;
    }

    public void AddMana3()
    {
        MaxMana3++;
    }

    public int Mana1
    {
        get { return _stats.Mana1; }
        set { _stats.Mana1 = value; }
    }

    public int Mana2
    {
        get { return _stats.Mana2; }
        set { _stats.Mana2 = value; }
    }

    public int Mana3
    {
        get { return _stats.Mana3; }
        set { _stats.Mana3 = value; }
    }

    public int MaxMana1
    {
        get { return _stats.MaxMana1; }
        set { _stats.MaxMana1 = value; }
    }

    public int MaxMana2
    {
        get { return _stats.MaxMana2; }
        set { _stats.MaxMana2 = value; }
    }

    public int MaxMana3
    {
        get { return _stats.MaxMana3; }
        set { _stats.MaxMana3 = value; }
    }
    #endregion

    #region Utility
    // Get ID
    private int GetID()
    {
        return ++_maxId;
    }

    private CardController FindCardControllerByID(int id)
    {
        CardController cardController;
        return _hand.TryGetValue(id, out cardController) ? cardController : null;
    }
    #endregion
}
