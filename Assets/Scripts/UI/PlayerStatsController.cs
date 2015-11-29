using Assets.Scripts.Core;
using Assets.Scripts.Core.Message;
using Assets.Scripts.Core.Resource;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.EventAggregator;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PlayerStatsController : MonoBehaviour, IHandle<ResourceChangeMessage>
    {
        public Text CrystalText;
        public Text DeuteriumText;
        public Text HpText;
        public Text MetalText;
        public Game.User User;

        public void Handle(ResourceChangeMessage message)
        {
            var resourceController = message.ResourceController;
            if (resourceController.Owner.User != User)
                return;
            Log.Verbose("PlayerStatsController: Handle ResourceChangeMessage");

            HpText.text = "HP: " + resourceController.GetResource(Resource.Hp, ResourceController.Type.Current) + " / "
                          + resourceController.GetResource(Resource.Hp, ResourceController.Type.Maximum);
            MetalText.text = "Metal: " + resourceController.GetResource(Resource.Metal, ResourceController.Type.Current) +
                             " / "
                             + resourceController.GetResource(Resource.Metal, ResourceController.Type.Maximum);
            CrystalText.text = "Crystal: " +
                               resourceController.GetResource(Resource.Crystal, ResourceController.Type.Current) + " / "
                               + resourceController.GetResource(Resource.Crystal, ResourceController.Type.Maximum);
            DeuteriumText.text = "Deuterium: " +
                                 resourceController.GetResource(Resource.Deuterium, ResourceController.Type.Current) +
                                 " / "
                                 + resourceController.GetResource(Resource.Deuterium, ResourceController.Type.Maximum);
        }

        private void Awake()
        {
        }
    }
}