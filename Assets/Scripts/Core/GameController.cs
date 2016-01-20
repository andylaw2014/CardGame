using System.Collections.Generic;
using Assets.Scripts.Core.Message;
using Assets.Scripts.Gui;
using Assets.Scripts.Gui.Event;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.EventAggregator;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameController : MonoBehaviour, IHandle<PhaseStartMessage>, IHandle<EnableResourcePanelMessage>,
        IHandle<PlayerStatsChangeMessage>, IHandle<CardZoneChangeMessage>, IHandle<CardDeadMessage>, IHandle<CardStatsChangeMessage>
    {
        private Game _game;
        public GuiMediator GuiMediator;
        public bool StartGame;

        private void Awake()
        {
            // TODO: Random Start
            _game = new Game(this, PhotonNetwork.isMasterClient ? PlayerType.Player : PlayerType.Opponent);
            _game.Subscribe(this);
            GuiMediator.OnButtonClick += OnButtonClick;
            GuiMediator.SetButtonClickable(ButtonType.NextPhaseButton, PhotonNetwork.isMasterClient);
            GuiMediator.OnCardDragToZone += OnCardDragToZone;
            GuiMediator.OnCardDragToCard += OnCardDragToCard;
        }

        private void OnButtonClick(object sender, ButtonClickEventArgs args)
        {
            Log.Verbose("OnButtonClick");
            var type = args.Type;
            if (type != ButtonType.NextPhaseButton) return;
            NextPhase();
        }

        private void Start()
        {
            if (StartGame)
                _game.Start();
        }

        private void OnCardDragToZone(object sender, CardDragToZoneEventArgs args)
        {
            Log.Verbose("OnCardDragToZone");
            _game.Handle(args);
        }

        private void OnCardDragToCard(object sender, CardDragToCardEventArgs args)
        {
            Log.Verbose("OnCardDragToCard");
            _game.Handle(args);
        }

        private void SetFrontAndDrag(string id, PlayerType owner, ZoneType destination)
        {
            var front = !(owner == PlayerType.Opponent && destination == ZoneType.Hand);
            GuiMediator.SetCardIsFront(id, front);
            var drag = (owner == PlayerType.Player && destination == ZoneType.Hand);
            SetDraggable(id, drag);
        }

        public void SelectAttacker(PlayerType player, string[] selectable)
        {
            Log.Verbose("SelectAttacker");
            GuiMediator.EnableSelection(attacker => { CreateBattle(player, attacker); }
                , selectable, true, false);
        }

        public void SetColor(string id, ColorType colorType)
        {
            GuiMediator.SelectColor(id, colorType);
        }

        public void SetDraggable(string id, bool drag)
        {
            GuiMediator.SetDraggable(id, drag);
        }

        #region Handle

        public void Handle(EnableResourcePanelMessage message)
        {
            GuiMediator.EnableResourcePanel(rType => { AddResource(message.Player, rType, 1); },
                message.EnableMetal, message.EnableCrystal, message.EnableMetal);
        }

        public void Handle(PhaseStartMessage message)
        {
            var phase = message.Phase;
            GuiMediator.SetButtonClickable(ButtonType.NextPhaseButton, phase.Parent == PlayerType.Player);
            GuiMediator.SetPhaseText(phase.GetName());
        }

        public void Handle(PlayerStatsChangeMessage message)
        {
            var player = message.Player;
            GuiMediator.UpdatePlayerStats(player.Type, player.GePlayerStats());
        }

        public void Handle(CardZoneChangeMessage message)
        {
            var card = message.Card;
            Log.Verbose(string.Format("Handle CardZoneChangeMessage:{0},{1},{2}", card.Id, card.Parent, card.Zone));
            GuiMediator.MoveCard(card.Id, card.Parent, card.Zone);
            SetFrontAndDrag(card.Id, card.Parent, card.Zone);
        }

        public void Handle(CardDeadMessage message)
        {
            GuiMediator.DestoryCard(message.Card.Id);
        }

        public void Handle(CardStatsChangeMessage message)
        {
            var card = message.Card;
            GuiMediator.UpdateCardStats(card.Id,card.GetStatistics());
        }

        #endregion

        #region Photon

        public void NextPhase()
        {
            Log.Verbose("NextPhase (RPC)");
            GetComponent<PhotonView>().RPC("RpcNextPhase", PhotonTargets.AllViaServer);
        }

        [PunRPC]
        private void RpcNextPhase()
        {
            _game.NextPhase();
        }

        public void AddResource(PlayerType pType, ResourceType rType, int value)
        {
            Log.Verbose("AddResource (RPC)");
            var bytePlayerType = (byte) pType;
            var byteOpponentType = (byte) pType.Opposite();
            var byteResourceType = (byte) rType;

            RpcAddResource(bytePlayerType, byteResourceType, value);
            GetComponent<PhotonView>()
                .RPC("RpcAddResource", PhotonTargets.Others, byteOpponentType, byteResourceType, value);
        }

        [PunRPC]
        private void RpcAddResource(byte bytePlayerType, byte byteResourceType, int value)
        {
            var pType = (PlayerType) bytePlayerType;
            var rType = (ResourceType) byteResourceType;
            _game.AddResource(pType, rType, value);
        }

        public void CreateCard(string cardName, string id, PlayerType owner, ZoneType destination)
        {
            var bytePlayerType = (byte) owner;
            var byteOpponentType = (byte) owner.Opposite();
            var byteDestinationType = (byte) destination;
            RpcCreateCard(cardName, id, bytePlayerType, byteDestinationType);
            GetComponent<PhotonView>()
                .RPC("RpcCreateCard", PhotonTargets.Others, cardName, id, byteOpponentType, byteDestinationType);
        }

        [PunRPC]
        private void RpcCreateCard(string cardName, string id, byte byteOwner, byte byteDestination)
        {
            var owner = (PlayerType) byteOwner;
            var destination = (ZoneType) byteDestination;
            Log.Verbose(string.Format("CreateCard (RPC):{0},{1}", id, owner));
            _game.CreateCard(owner, destination, id, GuiMediator.CreateCard(cardName, id, owner, destination));
            SetFrontAndDrag(id, owner, destination);
        }

        public void PlayCard(string id)
        {
            GetComponent<PhotonView>().RPC("RpcPlayCard", PhotonTargets.Others, id);
        }

        [PunRPC]
        private void RpcPlayCard(string id)
        {
            _game.PlayCard(id);
        }

        public void CreateBattle(PlayerType player, string[] attacker)
        {
            var bytePlayerType = (byte) player;
            var byteOpponentType = (byte) player.Opposite();
            RpcCreateBattle(bytePlayerType, attacker);
            GetComponent<PhotonView>().RPC("RpcCreateBattle", PhotonTargets.Others, byteOpponentType, attacker);
        }

        [PunRPC]
        private void RpcCreateBattle(byte bytePlayerType, string[] attacker)
        {
            var player = (PlayerType) bytePlayerType;
            if(attacker!=null)
            _game.CreateBattle(player, attacker);
        }

        public void AddBattle(string defender, string attacker)
        {
            GetComponent<PhotonView>().RPC("RpcAddBattle", PhotonTargets.All, defender, attacker);
        }

        [PunRPC]
        private void RpcAddBattle(string defender, string attacker)
        {
            _game.AddFight(defender, attacker);
        }

        #endregion


    }
}