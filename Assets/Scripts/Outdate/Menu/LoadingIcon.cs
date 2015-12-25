using UnityEngine;
using UnityEngine.UI;

// Use for loading icon animation only.

namespace Assets.Scripts.Outdate.Menu
{
    public class LoadingIcon : MonoBehaviour
    {
        // Sprites array of different frames.
        public Sprite[] Frames;
        // FPS of sprites.
        public int FramesPerSecond;

        private void OnGUI()
        {
            // Change sprite depend on time.
            var index = (int) ((Time.time*FramesPerSecond)%Frames.Length);
            GetComponent<Image>().sprite = Frames[index];
        }
    }
}