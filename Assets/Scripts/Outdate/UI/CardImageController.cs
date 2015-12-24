using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Control card image between front image and card back. 
namespace Assets.Scripts.Outdate.UI
{
    public class CardImageController : MonoBehaviour, IPointerEnterHandler
    {
        private bool _isFront;
        private Image _image;
        public Sprite Front;
        public Sprite Back; // Front image of card.

        [HideInInspector]
        public bool IsFront
        {
            get { return _isFront; }

            set
            {
                _isFront = value;
                GetComponent<Image>().sprite = _isFront ? Front : Back;
            }
        }

        private void Awake()
        {
            _image = GameObject.FindGameObjectWithTag("CardView").GetComponentsInChildren<Image>()[1];
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isFront)
                _image.sprite = Front;
        }
    }
}