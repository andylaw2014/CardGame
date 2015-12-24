using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Outdate
{
    public class CardViewController : MonoBehaviour
    {
        public Image ImageView;

        public void SetImage(Sprite image)
        {
            ImageView.sprite = image;
        }
    }
}