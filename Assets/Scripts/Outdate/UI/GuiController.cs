using System.Collections.Generic;
using Assets.Scripts.Outdate.Core;
using Assets.Scripts.Outdate.Core.Message;
using Assets.Scripts.Outdate.Infrastructure;
using Assets.Scripts.Outdate.Infrastructure.EventAggregator;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Outdate.UI
{
    public class GuiController : MonoBehaviour, IHandle<StartPhaseMessage>, IHandle<DrawCardMessage>,IHandle<PlayCardMessage>
    {
        private GameController _gameController;
        private Dictionary<string, GameObject> _cardCache;
        public Button NextPhaseButton;
        public PlayerController OpponentCtr;
        public Text PhaseText;
        public PlayerController PlayerCtr;
        public ResourcePanelController ResourcePanelController;

        public void ShowResourcePanelController()
        {
            ResourcePanelController.Activate();
        }

        public void Handle(DrawCardMessage message)
        {
            Log.Verbose("GuiController: Handle DrawCardMessage");
            var user = message.Player.User;
            var card = message.Card;
            DrawCard(card.Name, card.Id, user);
        }

        public void Handle(StartPhaseMessage message)
        {
            Log.Verbose("GuiController: Handle StartPhaseMessage");
            NextPhaseButton.interactable = message.Phase.Owner == Game.User.You;
            PhaseText.text = message.Phase.ToString();
        }

        public PlayerController GetPlayerController(Game.User who)
        {
            return who == Game.User.You ? PlayerCtr : OpponentCtr;
        }

        public void DrawCard(string cardName, string id, Game.User user)
        {
            Log.Verbose("DrawCard: " + cardName);
            var owner = GetPlayerController(user);
            var card = Instantiate(Resources.Load(cardName)) as GameObject;
            card.name = id;
            card.GetComponent<CardImageController>().IsFront = user == Game.User.You;
            if (user == Game.User.You)
                card.AddComponent<Draggable>();
            owner.AddToHand(card);
            _cardCache.Add(id, card);
            _gameController.Game.GetCardById(id).SetCard(card.GetComponent<CardController>());
        }

        public GameObject GetGameObjectById(string id)
        {
            GameObject card;
            return _cardCache.TryGetValue(id, out card) ? card : null;
        }

        private void Awake()
        {
            _gameController = GetComponent<GameController>();
            _cardCache = new Dictionary<string, GameObject>();
            NextPhaseButton.onClick.AddListener(() =>
            {
                NextPhaseButton.interactable = false;
                GetComponent<PhotonView>().RPC("NextPhase", PhotonTargets.AllViaServer);
            });
        }

        public void Handle(PlayCardMessage message)
        {
            var user = GetPlayerController(message.Player.User);
            GameObject card;
            if (!_cardCache.TryGetValue(message.Card.Id, out card)) return;
            user.AddToMinion(card);
            card.GetComponent<CardImageController>().IsFront = true;
        }
    }
}