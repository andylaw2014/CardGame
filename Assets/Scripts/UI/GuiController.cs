using Assets.Scripts.Core;
using Assets.Scripts.Core.Message;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.EventAggregator;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GuiController : MonoBehaviour, IHandle<StartPhaseMessage>
    {
        private GameController _gameController;
        public Button NextPhaseButton;
        public Text PhaseText;

        public void Handle(StartPhaseMessage message)
        {
            Log.Verbose("GuiController: Handle StartPhaseMessage");
            NextPhaseButton.interactable = message.Phase.Owner == Game.User.You;
            PhaseText.text = message.Phase.ToString();
        }

        private void Awake()
        {
            _gameController = GetComponent<GameController>();
            NextPhaseButton.onClick.AddListener(() =>
            {
                NextPhaseButton.interactable = false;
                GetComponent<PhotonView>().RPC("NextPhase", PhotonTargets.All);
            });
        }
    }
}