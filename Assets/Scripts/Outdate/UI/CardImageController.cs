using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Control card image between front image and card back. 

namespace Assets.Scripts.Outdate.UI
{
    public class CardImageController : MonoBehaviour, IPointerEnterHandler
    {
        private Image _image;
        private bool _isFront;
        public Sprite Back; // Front image of card.
        public Sprite Front;

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

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isFront)
                _image.sprite = Front;
        }

        private void Awake()
        {
            _image = GameObject.FindGameObjectWithTag("CardView").GetComponentsInChildren<Image>()[1];
        }
    }
}