using UnityEngine;
using UnityEngine.UI;

// For Quit button only.
public class QuitButton : MonoBehaviour
{
    void Start()
    {
        // Add OnClick listener
        GetComponent<Button>().onClick.AddListener(() => OnClick());
    }
    
    // End program. 
    private void OnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
