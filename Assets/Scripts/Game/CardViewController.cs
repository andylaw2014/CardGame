using UnityEngine;
using UnityEngine.UI;

public class CardViewController : MonoBehaviour
{

    public Image ImageView;

    public void SetImage(Sprite image)
    {
        ImageView.sprite = image;
    }
}
