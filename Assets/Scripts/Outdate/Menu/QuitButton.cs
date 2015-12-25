using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

// For Quit button only.

namespace Assets.Scripts.Outdate.Menu
{
    public class QuitButton : MonoBehaviour
    {
        private void Start()
        {
            // Add OnClick listener
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        // End program. 
        private void OnClick()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
        }
    }
}