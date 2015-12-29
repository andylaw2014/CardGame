using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.EventAggregator;
using Assets.Scripts.Outdate.Core;
using Assets.Scripts.Outdate.Core.Message;
using Assets.Scripts.Outdate.Core.Resource;
using Assets.Scripts.Outdate.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Outdate.UI
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