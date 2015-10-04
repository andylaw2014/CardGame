using UnityEngine;
using UnityEngine.UI;

public class AddManaPanelController : MonoBehaviour
{
    public Button Mana1Button;
    public Button Mana2Button;
    public Button Mana3Button;
    public GameController GameController;

    void Start()
    {
        Mana1Button.onClick.AddListener(AddMana1);
        Mana2Button.onClick.AddListener(AddMana2);
        Mana3Button.onClick.AddListener(AddMana3);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        Mana1Button.interactable = !GameController.IsMana1Full;
        Mana2Button.interactable = !GameController.IsMana2Full;
        Mana3Button.interactable = !GameController.IsMana3Full;
    }

    public void AddMana1()
    {
        GameController.AddMana1();
        Deactivate();
    }

    public void AddMana2()
    {
        GameController.AddMana2();
        Deactivate();
    }

    public void AddMana3()
    {
        GameController.AddMana3();
        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
