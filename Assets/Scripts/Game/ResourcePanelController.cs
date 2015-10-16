using UnityEngine;
using UnityEngine.UI;

public class ResourcePanelController : MonoBehaviour
{
    public Button MetalButton;
    public Button CrystalButton;
    public Button DeuteriumButton;

    void Awake()
    {
        MetalButton.onClick.AddListener(() => AddResource(EnumType.Resource.Metal));
        CrystalButton.onClick.AddListener(() => AddResource(EnumType.Resource.Crystal));
        DeuteriumButton.onClick.AddListener(() => AddResource(EnumType.Resource.Deuterium));
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        MetalButton.interactable = !GameController.Instance.Player.IsResourceFull(EnumType.Resource.Metal);
        CrystalButton.interactable = !GameController.Instance.Player.IsResourceFull(EnumType.Resource.Crystal);
        DeuteriumButton.interactable = !GameController.Instance.Player.IsResourceFull(EnumType.Resource.Deuterium);
    }

    private void AddResource(EnumType.Resource resource)
    {
        GameController.Instance.AddResource(resource,true);
        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
