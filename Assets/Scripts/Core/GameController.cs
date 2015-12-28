using Assets.Scripts.Core.Event;
using Assets.Scripts.Gui;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameController : MonoBehaviour
    {
        private Game _game;
        public GuiMediator GuiMediator;

        private void Awake()
        {
            // TODO: Random Start
            _game = new Game(this, PhotonNetwork.isMasterClient ? PlayerType.Player : PlayerType.Opponent);
            _game.OnPhaseChange += OnPhaseChange;
            _game.OnCardMove += OnCardMove;
            _game.OnPlayerStatsChange += OnPlayerStatsChange;
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

        public void EnableResourcePanel(bool metalEnable = true, bool crystalEnable = true, bool deuteriumEnable = true)
        {
            
        }
    }
}