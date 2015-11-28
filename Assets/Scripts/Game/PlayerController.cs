using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int MaxResource = 10;
    private Dictionary<int, CardController> _board;

    private Dictionary<int, CardController> _hand;
    private bool _isPlayer;
    private int _maxId;
    public GameObject HandZone;
    public GameObject MinionZone;
    public PlayerStatisticController Stats;

    private void Awake()
    {
        _hand = new Dictionary<int, CardController>();
        _board = new Dictionary<int, CardController>();
        _isPlayer = CompareTag("Player");
        _maxId = -1;
    }

    public int DrawCard(GameObject card, int id = -1)
    {
        var cardController = card.GetComponent<CardController>();
        cardController.IsFront = _isPlayer;
        cardController.AllowToPlay = _isPlayer;

        var mid = id == -1 ? GetId() : id;
        if (mid > _maxId)
            _maxId = mid;
        cardController.Id = mid;
        if (_isPlayer)
            card.AddComponent<PlayCard>();
        _hand.Add(mid, cardController);
        cardController.SetParent(HandZone.transform);
        return mid;
    }

    public bool IsPlayable(int id)
    {
        var cardController = FindCardControllerByIdInHand(id);
        if (!cardController.IsPlayable())
            return false;
        foreach (EnumType.Resource resource in Enum.GetValues(typeof (EnumType.Resource)))
        {
            if (Stats[resource] < cardController.GetResource(resource))
                return false;
        }
        return true;
    }

    public bool IsAttackable(int id)
    {
        var cardController = FindCardControllerByIdInBoard(id);
        return cardController.IsAttackable();
    }

    public bool IsDefencable(int id)
    {
        var cardController = FindCardControllerByIdInBoard(id);
        return cardController.IsDefencable();
    }

    public void Play(int id)
    {
        var cardController = FindCardControllerByIdInHand(id);
        foreach (EnumType.Resource resource in Enum.GetValues(typeof (EnumType.Resource)))
        {
            Stats[resource] -= cardController.GetResource(resource);
        }

        // UNDONE: Event Card
        if (_isPlayer)
            Destroy(cardController.GetComponent<PlayCard>());
        cardController.TogglePlayableGlow(false);
        cardController.AllowToPlay = false;
        cardController.SetParent(MinionZone.transform);
        cardController.IsFront = true;
        cardController.Play();

        _hand.Remove(id);
        _board.Add(id, cardController);

        TogglePlayableEffect(true);
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
        foreach (EnumType.Resource resource in Enum.GetValues(typeof (EnumType.Resource)))
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

    public void TogglePlayableEffect(bool turnOn)
    {
        foreach (var card in _hand)
        {
            if (turnOn && GameController2.Instance.IsCardPlayable(card.Key))
                card.Value.TogglePlayableGlow(true);
            else
                card.Value.TogglePlayableGlow(false);
        }
    }

    public void ToggleAttackableEffect(bool turnOn)
    {
        foreach (var card in _board)
        {
            if (GameController2.Instance.IsCardAttackable(card.Key) && turnOn)
            {
                card.Value.Selectable = true;
                card.Value.ToggleSelectableGlow(true);
            }
            else
            {
                card.Value.Selectable = false;
                card.Value.ToggleSelectableGlow(false);
            }
        }
    }


    public void ToggleAttackingEffect(bool turnOn)
    {
        Debug.Log("T" + GameController2.Instance.Combat.SelectAttackSet.Count);
        foreach (var id in GameController2.Instance.Combat.SelectAttackSet)
        {
            Debug.Log(id);
            var card = FindCardControllerByIdInBoard(id);
            card.ToggleAttackGlow(turnOn);
        }
    }

    public void ToggleDefencableEffect(bool turnOn)
    {
        foreach (var card in _board)
        {
            if (GameController2.Instance.IsCardDefencable(card.Key) && turnOn)
            {
                card.Value.Selectable = true;
                card.Value.ToggleSelectableGlow(true);
            }
            else
            {
                card.Value.Selectable = false;
                card.Value.ToggleSelectableGlow(false);
            }
        }
    }

    public void AddAttackors()
    {
        foreach (var card in _board)
        {
            if (card.Value.IsSelected())
                GameController2.Instance.Combat.AddAttackor(card.Key);
        }
    }

    private int GetId()
    {
        return ++_maxId;
    }

    public CardController FindCardControllerByIdInHand(int id)
    {
        CardController cardController;
        return _hand.TryGetValue(id, out cardController) ? cardController : null;
    }

    public CardController FindCardControllerByIdInBoard(int id)
    {
        CardController cardController;
        return _board.TryGetValue(id, out cardController) ? cardController : null;
    }
}