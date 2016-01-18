using Assets.Scripts.Core.Event;
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
        IHandle<PlayerStatsChangeMessage>
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

        private void OnCardMove(object sender, CardChangeEventArg args)
        {
            var card = args.Card;
            GuiMediator.MoveCard(card.Id, card.Parent, card.Zone);
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
            _game.CreateCard(owner, destination, id, GuiMediator.CreateCard(cardName, id, owner, destination));

            var front = !(owner == PlayerType.Opponent && destination == ZoneType.Hand);
            GuiMediator.SetCardIsFront(id, front);
        }

        #endregion
    }
}