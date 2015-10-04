using UnityEngine;
using UnityEngine.UI;

// Use for loading icon animation only.
public class LoadingIcon : MonoBehaviour
{
    // Sprites array of different frames.
    public Sprite[] Frames;
    // FPS of sprites.
    public int FramesPerSecond;

    void OnGUI()
    {
        // Change sprite depend on time.
        int index = (int)((Time.time * FramesPerSecond) % Frames.Length);
        GetComponent<Image>().sprite = Frames[index];
    }
}
