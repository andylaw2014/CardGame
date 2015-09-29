using UnityEngine;
using UnityEngine.UI;

// Use for loading icon animation only.
public class LoadingIcon : MonoBehaviour
{
    // Image to hold the sprites.
    public Image image;
    // Sprites array of different frames.
    public Sprite[] frames;
    // FPS of sprites.
    public int framesPerSecond;

    void OnGUI()
    {
        // Change sprite depend on time.
        int index = (int)((Time.time * framesPerSecond) % frames.Length);
        image.sprite = frames[index];
    }
}
