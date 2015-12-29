using System;
using Assets.Scripts.Core.Event;
using Assets.Scripts.Gui;
using Assets.Scripts.Gui.Event;
using Assets.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameController : MonoBehaviour
    {
        private Game _game;
        public GuiMediator GuiMediator;
        public bool StartGame;

        private void Awake()
        {
            // TODO: Random Start
            _game = new Game(this, PhotonNetwork.isMasterClient ? PlayerType.Player : PlayerType.Opponent);
            _game.OnPhaseChange += OnPhaseChange;
            _game.OnCardMove += OnCardMove;
            _game.OnPlayerStatsChange += OnPlayerStatsChange;
            GuiMediator.OnButtonClick += OnButtonClick;
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

        private void OnPlayerStatsChange(object sender, PlayerChangeEventArg args)
        {
            var player = args.Player;
            GuiMediator.UpdatePlayerStats(player.Type, player.GePlayerStats());
        }

        private void OnPhaseChange(object sender, PhaseChangeEventArg args)
        {
            GuiMediator.SetPhaseText(args.Phase.GetName());
        }

        private void OnCardMove(object sender, CardChangeEventArg args)
        {
            var card = args.Card;
            GuiMediator.MoveCard(card.Id, card.Parent, card.Zone);
        }

        public Gui.Card CreateCard(string cardName, string id, PlayerType owner, ZoneType destination)
        {
            return GuiMediator.CreateCard(cardName, id, owner, destination);
        }

        public void EnableResourcePanel(Action<ResourceType> onClose, bool metalEnable, bool crystalEnable,
            bool deuteriumEnable)
        {
            GuiMediator.EnableResourcePanel(onClose, metalEnable, crystalEnable, deuteriumEnable);
        }

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
            var byteResourceType = (byte) rType;
            GetComponent<PhotonView>()
                .RPC("RpcAddResource", PhotonTargets.Others, bytePlayerType, byteResourceType, value);
            RpcAddResource(bytePlayerType, byteResourceType, value);
        }

        [PunRPC]
        public void RpcAddResource(byte bytePlayerType, byte byteResourceType, int value)
        {
            var pType = (PlayerType) bytePlayerType;
            var rType = (ResourceType) byteResourceType;
            _game.AddResource(pType, rType, value);
        }

        #endregion
    }
}