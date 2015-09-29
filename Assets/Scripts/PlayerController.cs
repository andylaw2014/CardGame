using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private GameObject HandZone;
    private GameObject MinionZone;
    private List<int> Hand;
    private StatisticController Stats;
    private const int MAX_MANA = 10;
    void Start()
    {
        HandZone = gameObject.FindChildWithTag("HandZone");
        MinionZone = gameObject.FindChildWithTag("MinionZone");
        Stats = gameObject.FindComponentInChildWithTag<StatisticController>("Stat");
        Hand = new List<int>();
    }

    public int DrawCard(string cardName, int ID = -1, bool SetFront = true)
    {
        int id;
        GameObject card = Instantiate(Resources.Load(cardName)) as GameObject;
        if (ID == -1)
            id = UniqueID.GetID();
        else
            id = ID;
        card.GetComponent<UniqueID>().ID = id;
        if (SetFront)
        {
            card.GetComponent<CardImageController>().SetFront();
            card.AddComponent<Draggable>();
        }
        else
            card.GetComponent<CardImageController>().SetBack();
        card.transform.SetParent(HandZone.transform);
        card.transform.localScale = Vector3.one;
        Hand.Add(id);
        return id;
    }

    #region Mana
    public void ResetMana()
    {
        Stats.Mana1 = Stats.MaxMana1;
        Stats.Mana2 = Stats.MaxMana2;
        Stats.Mana3 = Stats.MaxMana3;
    }

    public bool IsMana1Full
    {
        get { return Stats.MaxMana1 >= 10; }
    }


    public bool IsMana2Full
    {
        get { return Stats.MaxMana2 >= 10; }
    }


    public bool IsMana3Full
    {
        get { return Stats.MaxMana3 >= 10; }
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
}
