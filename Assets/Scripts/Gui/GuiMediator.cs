using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Statistics;
using Assets.Scripts.Gui.Controller;
using Assets.Scripts.Gui.Event;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
    /// <summary>
    ///     Use to interact between UI elements and game core.
    /// </summary>
    public class GuiMediator : MonoBehaviour
    {
        // You should store the card id and the referenced gameobject
        private Dictionary<string, GameObject> _idDictionary;
        public Sprite CardBack;
        public Image CardView;
        public ResourcePanelController ResourcePanelController;

        /// <summary>
        ///     This is call when a button is clicked.
        /// </summary>
        /// <remarks>Do not remove the empty function.</remarks>
        public event EventHandler<ButtonClickEventArgs> OnButtonClick = (sender, args) => { };

        /// <summary>
        ///     This is call when a card drag to another card.
        /// </summary>
        /// <remarks>Do not remove the empty function.</remarks>
        public event EventHandler<CardDragToCardEventArgs> OnCardDragToCard = (sender, args) => { };

        /// <summary>
        ///     This is call when a card drag to another zone.
        /// </summary>
        /// <remarks>Do not remove the empty function.</remarks>
        public event EventHandler<CardDragToZoneEventArgs> OnCardDragToZone = (sender, args) => { };

        /// <summary>
        ///     Use this for initialization
        /// </summary>
        private void Awake()
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
            // TODO: Update player statistics
        }

        /// <summary>
        ///     Set a button if it is clickable.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="clickable"></param>
        public void SetButtonClickable(ButtonType type, bool clickable)
        {
            // TODO: Set button clickable
        }

        /// <summary>
        ///     Create a card (GameObject) and return its Card component.
        ///     Assign the card with given id.
        ///     The card created should place at given zone.
        /// </summary>
        /// <param name="cardName">Name of the card</param>
        /// <param name="id"></param>
        /// <param name="owner">Owner of the card</param>
        /// <param name="destination"></param>
        /// <returns>Card component</returns>
        public Card CreateCard(string cardName, string id, PlayerType owner, ZoneType destination)
        {
            // TODO: Create card
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
            // TODO: Move card
        }

        /// <summary>
        ///     Set the text of Phase Text
        /// </summary>
        /// <param name="text"></param>
        public void SetPhaseText(string text)
        {
            // TODO: Set phase text
        }

        /// <summary>
        ///     Set the card IsFront property.
        /// </summary>
        /// <param name="id">Card id.</param>
        /// <param name="isFront"></param>
        public void SetCardIsFront(string id, bool isFront)
        {
            // TODO: Set card IsFront
        }

        /// <summary>
        ///     Enable ResourcePanel and call onClose after a button is clicked.
        ///     The bools is to decide whether the button is clickable or not.
        /// </summary>
        /// <param name="onClose"></param>
        /// <param name="metalEnable"></param>
        /// <param name="crystalEnable"></param>
        /// <param name="deuteriumEnable"></param>
        public void EnableResourcePanel(Action<ResourceType> onClose, bool metalEnable,
            bool crystalEnable, bool deuteriumEnable)
        {
            // TODO: Enable ResourcePanel
            // call onClose(ResourceType t) after a button is clicked where t is the button clicked.
        }

        /// <summary>
        ///     Enable a selection of card and call onClose after selection.
        ///     Other than card id, Keyword.TargetPlayer and Keyword.TargetOpponent should be allowed.
        /// </summary>
        /// <param name="onClose"></param>
        /// <param name="idList">ID of the cards which is able to select.</param>
        /// <param name="allowMultiple">Allow multiple selections.</param>
        /// <param name="allowCancel">Allow to cacel the selection</param>
        public void EnableSelection(Action<string[]> onClose, string[] idList, bool allowMultiple,
            bool allowCancel)
        {
            // TODO:Enable Selection
            // You need a new UI element that allow the selection of cards with given ID.
            // At the end of selection (or cancel if allowed), call onClose (string[] selected)
            // selected is the arrays of selected card id(s). It should be null if it is canceled.
        }

        /// <summary>
        ///     Set a card to be draggable or not.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="draggable"></param>
        public void SetDraggable(string id, bool draggable)
        {
            // TODO: Set Draggable
        }
    }
}