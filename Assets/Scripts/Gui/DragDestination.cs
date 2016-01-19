using System;
using Assets.Scripts.Core;
using Assets.Scripts.Gui.Event;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gui
{
    public class DragDestination : MonoBehaviour, IEndDragHandler
    {
        private string _destination;
        private string _name;
        private string _id;
        public event EventHandler<CardDragToCardEventArgs> OnCardDragToCard;
        public event EventHandler<CardDragToZoneEventArgs> OnCardDragToZone;

        public void SetEvent(EventHandler<CardDragToCardEventArgs> e1, EventHandler<CardDragToZoneEventArgs> e2, string id)
        {
            OnCardDragToCard = e1;
            OnCardDragToZone = e2;
            _id = id;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _destination = eventData.pointerEnter.name;
            _name = eventData.pointerEnter.transform.parent.name;
            if (_name == "Opponent" || _name == "Player")
            {
                var zoneType = _destination == "Hand" ? ZoneType.Hand : ZoneType.BattleField;
                var ownerType = eventData.pointerEnter.transform.parent.name == "Player"
                    ? PlayerType.Player
                    : PlayerType.Opponent;
                if (OnCardDragToZone == null) return;
                OnCardDragToZone(this, new CardDragToZoneEventArgs(_id, zoneType, ownerType));
                //Debug.Log("_destination:  " + zoneType);
                //Debug.Log("_onwer:  " + ownerType);
            }
            else
            {
                if (OnCardDragToCard == null) return;
                OnCardDragToCard(this, new CardDragToCardEventArgs(_id, eventData.pointerEnter.GetComponent<Card>().Id));
                //Debug.Log("_Target :  " + _id);
                //Debug.Log("_Another Card:  " + eventData.pointerEnter.GetComponent<Card>().Id);
            }
        }
    }
}
