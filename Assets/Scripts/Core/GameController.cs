using Assets.Scripts.Infrastructure;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameController : MonoBehaviour
    {
        public GuiController GuiController;
        public Game Game { get; private set; }
        public PlayerStatsController Player;
        public PlayerStatsController Opponent;

        private void Awake()
        {
            // TODO: Random
            var first = PhotonNetwork.isMasterClient ? Game.User.You : Game.User.Opponent;
            Game = new Game(this, first);
            Game.Subscribe(GuiController);
            Game.Subscribe(Player);
            Game.Subscribe(Opponent);
        }

        private void Start()
        {
            Log.Verbose("====================Start====================");
            Game.Start();
        }

        [PunRPC]
        public void NextPhase()
        {
            Game.NextPhase();
        }

        [PunRPC]
        private void DrawCard(string cardName, string id, bool isYou)
        {
            var user = isYou ? Game.User.You : Game.User.Opponent;
            Game.DrawCard(cardName, id , user);
        }

        public void RpcDrawCard(string cardName, bool isYou)
        {
            Log.Verbose("RpcDrawCard");
            var id = Game.GenerateId();
            GetComponent<PhotonView>().RPC("DrawCard", PhotonTargets.Others, cardName, id, !isYou);
            DrawCard(cardName, id, isYou);
        }
        
        [PunRPC]
        private void PlayCard(string id)
        {
            Game.PlayCardFromHand(id);
        }

        public void RpcPlayCard(string id)
        {
            Log.Verbose("RpcPlayCard");
            GetComponent<PhotonView>().RPC("PlayCard", PhotonTargets.AllViaServer, id);
        }

        [PunRPC]
        private void AddResource(int resource, bool isYou)
        {
            var res = (Resource.Resource) resource;
            var user = isYou ? Game.User.You : Game.User.Opponent;
            Game.GetPlayer(user).AddResource(res,true);
        }

        public void RpcAddResource(Resource.Resource resource)
        {
            var res = (int) resource;
            Log.Verbose("RpcAddResource");
            GetComponent<PhotonView>().RPC("AddResource", PhotonTargets.Others, res, false);
            AddResource(res, true);
        }
    }
}