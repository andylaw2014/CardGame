using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Outdate.DeckEdit
{
    public class ShowDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Sprite ImageView;

        public void OnPointerClick(PointerEventData eventData)
        {
            var d = eventData.pointerPress;
            var returnGameObject = d.transform.parent.gameObject;
            if (returnGameObject.tag.Equals("ChoiceDeck"))
            {
                var Chosen = GameObject.FindGameObjectWithTag("ChosenDeck");
                d.transform.SetParent(Chosen.transform, false);
            }
            else if (returnGameObject.tag.Equals("ChosenDeck"))
            {
                var Choice = GameObject.FindGameObjectWithTag("ChoiceDeck");
                d.transform.SetParent(Choice.transform, false);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            var cardViewController =
                GameObject.FindGameObjectWithTag("DetailCard").GetComponent<CardViewController>();
            cardViewController.SetImage(ImageView);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            var cardViewController =
                GameObject.FindGameObjectWithTag("DetailCard").GetComponent<CardViewController>();
            cardViewController.SetImage(null);
        }

        private void start()
        {
            ImageView = new Sprite();
        }
    }
}