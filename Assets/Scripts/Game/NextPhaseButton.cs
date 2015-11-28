using UnityEngine;
using UnityEngine.UI;

public class NextPhaseButton : MonoBehaviour
{
    public void NotClickable()
    {
        GetComponent<Button>().interactable = false;
    }
}