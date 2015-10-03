using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private GameObject HandZone;
    private GameObject MinionZone;
    private List<ValuePair<int, CardController>> Hand;
    private PlayerStatisticController Stats;
    private const int MAX_MANA = 10;

    private int maxID;
    void Start()
    {
        HandZone = gameObject.FindChildWithTag("HandZone");
        MinionZone = gameObject.FindChildWithTag("MinionZone");
        Stats = gameObject.FindComponentInChildWithTag<PlayerStatisticController>("Stat");
        Hand = new List<ValuePair<int, CardController>>();
        maxID = 0;
    }

    public int DrawCard(string cardName, int ID = -1, bool isPlayer = true)
    {
        GameObject card = Instantiate(Resources.Load(cardName)) as GameObject;
        CardController cardController = card.GetComponent<CardController>();

        cardController.SetImageSide(isPlayer);
        cardController.SetDraggable(isPlayer);

        int id = ID == -1 ? GetID() : ID;
        cardController.SetID(id);
        Hand.Add(new ValuePair<int, CardController>(id, cardController));

        cardController.SetParent(HandZone.transform);
        return id;
    }
    #region PlayCard
    // Is a card playable
    public bool IsPlayable(int id)
    {
        return FindCardControllerByID(id).IsPlayable();
    }

    public void Play(int id)
    {
        // UNDONE: Event Card
        CardController cardController = FindCardControllerByID(id);
        cardController.SetParent(MinionZone.transform);
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
        get { return Stats.MaxMana1 >= MAX_MANA; }
    }


    public bool IsMana2Full
    {
        get { return Stats.MaxMana2 >= MAX_MANA; }
    }


    public bool IsMana3Full
    {
        get { return Stats.MaxMana3 >= MAX_MANA; }
    }

    public bool IsManaFull
    {
        get { return IsMana1Full && IsMana2Full && IsMana3Full; }
    }

    public void AddMana1()
    {
        Stats.MaxMana1++;
    }

    public void AddMana2()
    {
        Stats.MaxMana2++;
    }

    public void AddMana3()
    {
        Stats.MaxMana3++;
    }
    #endregion

    // Get ID
    private int GetID()
    {
        maxID++;
        return maxID - 1;
    }

    private CardController FindCardControllerByID(int id)
    {
        foreach (ValuePair<int, CardController> pair in Hand)
        {
            if (pair.Value1 == id)
                return pair.Value2;
        }

        return null;
    }
}
