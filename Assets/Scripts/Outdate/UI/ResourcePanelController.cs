using Assets.Scripts.Outdate.Core;
using Assets.Scripts.Outdate.Core.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Outdate.UI
{
    public class ResourcePanelController : MonoBehaviour
    {
        public Button CrystalButton;
        public Button DeuteriumButton;
        public GameController GameController;
        public Button MetalButton;

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
            DeuteriumButton.interactable =
                !GameController.Game.GetPlayer(Game.User.You).IsResourceFull(Resource.Deuterium);
            gameObject.SetActive(MetalButton.interactable || CrystalButton.interactable || DeuteriumButton.interactable);
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