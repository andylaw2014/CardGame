using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private GameObject HandZone;
    private GameObject MinionZone;
    private Dictionary<int, CardController> Hand;
    private PlayerStatisticController Stats;
    private const int MAX_MANA = 10;

    private int maxID;
    void Start()
    {
        HandZone = gameObject.FindChildWithTag("HandZone");
        MinionZone = gameObject.FindChildWithTag("MinionZone");
        Stats = gameObject.FindComponentInChildWithTag<PlayerStatisticController>("Stat");
        Hand = new Dictionary<int, CardController>();
        maxID = -1;
    }

    public int DrawCard(string cardName, int id = -1, bool isPlayer = true)
    {
        GameObject card = Instantiate(Resources.Load(cardName)) as GameObject;
        CardController cardController = card.GetComponent<CardController>();

        cardController.IsFront = isPlayer;
        cardController.Draggable = isPlayer;

        int mid = id == -1 ? GetID() : id;
        cardController.ID = mid;
        Hand.Add(mid, cardController);
        cardController.SetParent(HandZone.transform);
        return id;
    }

    #region PlayCard
    // Is a card playable
    public bool IsPlayable(int id)
    {
        CardController cardToPlayController = FindCardControllerByID(id);
        bool enoughMana = (Mana1 >= cardToPlayController.Mana1Cost) && (Mana2 >= cardToPlayController.Mana2Cost) &&
                          (Mana3 >= cardToPlayController.Mana3Cost);
        return cardToPlayController.IsPlayable() && enoughMana;
    }

    public void Play(int id)
    {
        CardController cardToPlayController = FindCardControllerByID(id);
        Debug.Log((cardToPlayController == null) + ":" + id + ":" + Hand.ContainsKey(id));
        Mana1 -= cardToPlayController.Mana1Cost;
        Mana2 -= cardToPlayController.Mana2Cost;
        Mana3 -= cardToPlayController.Mana3Cost;

        // UNDONE: Event Card
        CardController cardController = FindCardControllerByID(id);
        cardController.Draggable = false;
        cardController.SetParent(MinionZone.transform);
        cardController.IsFront = true;
        cardController.Play();
    }
    #endregion

    #region Mana
    public void ResetMana()
    {
        Stats.Mana1 = Stats.MaxMana1;
        Stats.Mana2 = Stats.MaxMana2;
        Stats.Mana3 = Stats.MaxMana3;
    }

    public bool IsMana1Full
    {
        get { return Mana1 >= MAX_MANA; }
    }


    public bool IsMana2Full
    {
        get { return Mana2 >= MAX_MANA; }
    }


    public bool IsMana3Full
    {
        get { return Mana3 >= MAX_MANA; }
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
        get { return Stats.Mana1; }
        set { Stats.Mana1 = value; }
    }

    public int Mana2
    {
        get { return Stats.Mana2; }
        set { Stats.Mana2 = value; }
    }

    public int Mana3
    {
        get { return Stats.Mana3; }
        set { Stats.Mana3 = value; }
    }

    public int MaxMana1
    {
        get { return Stats.MaxMana1; }
        set { Stats.MaxMana1 = value; }
    }

    public int MaxMana2
    {
        get { return Stats.MaxMana2; }
        set { Stats.MaxMana2 = value; }
    }

    public int MaxMana3
    {
        get { return Stats.MaxMana3; }
        set { Stats.MaxMana3 = value; }
    }
    #endregion

    #region Utility
    // Get ID
    private int GetID()
    {
        return ++maxID;
    }

    private CardController FindCardControllerByID(int id)
    {
        CardController cardController;
        if (Hand.TryGetValue(id, out cardController))
            return cardController;
        return null;
    }
    #endregion
}
