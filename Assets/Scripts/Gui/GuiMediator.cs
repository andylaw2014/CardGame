using System;
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
        /// <summary>
        ///     This is call when a button is clicked.
        /// </summary>
        public event EventHandler<ButtonClickEventArgs> OnButtonClick = (sender, args) => { };

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
        /// Set a button if it is clickable.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="clickable"></param>
        public void SetButtonClickable(ButtonType type, bool clickable)
        {
            //TODO: Set button clickable
        }

        /// <summary>
        /// Create a card (gameobject) and return its "card" component.
        /// Assign the card with given id.
        /// The card created should place at given zone.
        /// </summary>
        /// <param name="name">Name of the card</param>
        /// <param name="id"></param>
        /// <param name="pType">Owner of the card</param>
        /// <param name="zType"></param>
        /// <returns></returns>
        public Card CreateCard(string name, string id, PlayerType pType, ZoneType zType)
        {
            //TODO: Create card
            throw new NotImplementedException();
        }
    }
}