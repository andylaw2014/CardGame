using Assets.Scripts.Core;
using Assets.Scripts.Core.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ResourcePanelController : MonoBehaviour
    {
        public Button CrystalButton;
        public Button DeuteriumButton;
        public Button MetalButton;
        public GameController GameController;

        private void Awake()
        {
            MetalButton.onClick.AddListener(() => AddResource(Resource.Metal));
            CrystalButton.onClick.AddListener(() => AddResource(Resource.Crystal));
            DeuteriumButton.onClick.AddListener(() => AddResource(Resource.Deuterium));
        }

        public void Activate()
        {
            MetalButton.interactable = !GameController.Game.GetPlayer(Game.User.You).IsResourceFull(Resource.Metal);
            CrystalButton.interactable = !GameController.Game.GetPlayer(Game.User.You).IsResourceFull(Resource.Crystal);
            DeuteriumButton.interactable = !GameController.Game.GetPlayer(Game.User.You).IsResourceFull(Resource.Deuterium);
            gameObject.SetActive(MetalButton.interactable|| CrystalButton.interactable || DeuteriumButton.interactable);
        }

        private void AddResource(Resource resource)
        {
            GameController.RpcAddResource(resource);
            Deactivate();
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}