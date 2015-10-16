using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public GameObject HandZone;
    public GameObject MinionZone;
    public PlayerStatisticController Stats;

    private Dictionary<int, CardController> _hand;
    private bool _isPlayer;
    private int _maxId;
    private const int MaxResource = 10;

    void Awake()
    {
        _hand = new Dictionary<int, CardController>();
        _isPlayer = CompareTag("Player");
        _maxId = -1;
    }

    public int DrawCard(GameObject card, int id=-1)
    {
        var cardController = card.GetComponent<CardController>();
        cardController.IsFront = _isPlayer;
        cardController.Draggable = _isPlayer;

        var mid = id == -1 ? GetId() : id;
        if (mid > _maxId)
            _maxId = mid;
        cardController.Id = mid;

        _hand.Add(mid, cardController);
        cardController.SetParent(HandZone.transform);
        return mid;
    }


    public bool IsPlayable(int id)
    {
        var cardController = FindCardControllerById(id);
        if (!cardController.IsPlayable())
            return false;
        foreach (EnumType.Resource resource in Enum.GetValues(typeof(EnumType.Resource)))
        {
            if (Stats[resource]< cardController.GetResource(resource))
                return false;
        }
        return true;
    }

    public void Play(int id)
    {
        var cardController = FindCardControllerById(id);
        foreach (EnumType.Resource resource in Enum.GetValues(typeof(EnumType.Resource)))
        {
            Stats[resource] -= cardController.GetResource(resource);
        }

        // UNDONE: Event Card
        cardController.Draggable = false;
        cardController.SetParent(MinionZone.transform);
        cardController.IsFront = true;
        cardController.Play();
    }

    public void ResetResource()
    {
        Stats.ResetResource();
    }

    public bool IsResourceFull(EnumType.Resource resource)
    {
        return Stats[resource] >= MaxResource;
    }

    public bool IsResourceAllFull()
    {
        foreach (EnumType.Resource resource in Enum.GetValues(typeof(EnumType.Resource)))
        {
            if (!IsResourceFull(resource))
                return false;
        }
        return true;
    }

    public void AddResource(EnumType.Resource resource)
    {
        Stats.Max[resource]++;
    }

    private int GetId()
    {
        return ++_maxId;
    }

    private CardController FindCardControllerById(int id)
    {
        CardController cardController;
        return _hand.TryGetValue(id, out cardController) ? cardController : null;
    }
}
