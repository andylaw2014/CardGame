using UnityEngine;
using UnityEngine.UI;

public class GameGuiController : MonoBehaviour
{
    public static GameGuiController Instance { get; private set; }

    public Text PhaseText; // GUI text to show current phase.
    public Button NextPhaseButton;  // Button to end current phase
    public Sprite CardBack;  // Card back

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this);
    }

    private void OnGUI()
    {
        PhaseText.text = GameController.Instance.Phase.StateText;
    }
}
