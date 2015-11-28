using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameController : MonoBehaviour
    {
        private GuiController _guiController;
        public Game Game { get; private set; }

        private void Awake()
        {
            // TODO: Random
            var first = PhotonNetwork.isMasterClient ? Game.User.You : Game.User.Opponent;
            Game = new Game(this, first);
            _guiController = GetComponent<GuiController>();
            Game.Subscribe(_guiController);
        }

        private void Start()
        {
            Game.Start();
        }

        [PunRPC]
        public void NextPhase()
        {
            Game.NextPhaseButton();
        }
    }
}