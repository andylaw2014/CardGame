using UnityEngine;
using UnityEngine.UI;

public class GameGuiController : MonoBehaviour
{
    public Sprite CardBack; // Card back
    public Button NextPhaseButton; // Button to end current phase

    public Text PhaseText; // GUI text to show current phase.
    public static GameGuiController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this);
    }

    private void OnGUI()
    {
        PhaseText.text = GameController2.Instance.Phase.StateText;
    }
}