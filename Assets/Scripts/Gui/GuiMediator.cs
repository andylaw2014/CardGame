using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Card;
using UnityEngine;

namespace Assets.Scripts.Gui
{
    /// <summary>
    ///     Use to interact between UI elements and game core.
    /// </summary>
    public class GuiMediator : MonoBehaviour
    {
        private Dictionary<string, GameObject> _idDictionary;
            // You should store the card id and the referenced gameobject

        /// <summary>
        ///     This is call when a button is clicked.
        /// </summary>
        public event EventHandler<ButtonClickEventArgs> OnButtonClick = (sender, args) => { }; // Do not remove the empty function

        /// <summary>
        ///     This is call when a card drag to another card.
        /// </summary>
        public event EventHandler<CardDragToCardEventArgs> OnCardDragToCard = (sender, args) => { }; // Do not remove the empty function

        /// <summary>
        ///     This is call when a card drag to another zone.
        /// </summary>
        public event EventHandler<CardDragToZoneEventArgs> OnCardDragToZone = (sender, args) => { }; // Do not remove the empty function

        private void Start()
        {
            _idDictionary = new Dictionary<string, GameObject>();
        }

        /// <summary>
        ///     Update player's statistics.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stats"></param>
        public void UpdatePlayerStats(PlayerType type, PlayerStats stats)
        {
            //TODO: Update player statistics
        }

        /// <summary>
        ///     Set a button if it is clickable.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="clickable"></param>
        public void SetButtonClickable(ButtonType type, bool clickable)
        {
            //TODO: Set button clickable
        }

        /// <summary>
        ///     Create a card (gameobject) and return its "card" component.
        ///     Assign the card with given id.
        ///     The card created should place at given zone.
        /// </summary>
        /// <param name="cardName">Name of the card</param>
        /// <param name="id"></param>
        /// <param name="owner">Owner of the card</param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public Card CreateCard(string cardName, string id, PlayerType owner, ZoneType destination)
        {
            //TODO: Create card
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Move a card to different area
        /// </summary>
        /// <param name="id"></param>
        /// <param name="owner"></param>
        /// <param name="destination"></param>
        public void MoveCard(string id, PlayerType owner, ZoneType destination)
        {
            //TODO: Move Card
        }
    }
}