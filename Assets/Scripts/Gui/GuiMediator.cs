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
        public Button NextButton;
        public Text PhaseText;
        public ResourcePanelController ResourcePanelController;
        public PlayerController Player;
        public PlayerController Opponent;

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
            NextButton.onClick.AddListener(() =>
            {
                OnButtonClick(this, new ButtonClickEventArgs(ButtonType.NextPhaseButton));
            });
        }

        /// <summary>
        ///     Update player's statistics.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stats"></param>
        public void UpdatePlayerStats(PlayerType type, PlayerStats stats)
        {
            // TODO: Update player statistics
            if (type == PlayerType.Player)
            {
                Player.Stats.SetText(PlayerStatsType.Hp, "HP: " + stats.Get(PlayerStatsType.Hp) + " / " + stats.Get(PlayerStatsType.MaxHp));
                Player.Stats.SetText(PlayerStatsType.Metal, "Metal: " + stats.Get(PlayerStatsType.Metal) + " / " + stats.Get(PlayerStatsType.MaxMetal));
                Player.Stats.SetText(PlayerStatsType.Deuterium, "Deuterium: " + stats.Get(PlayerStatsType.Deuterium) + " / " + stats.Get(PlayerStatsType.MaxDeuterium));
                Player.Stats.SetText(PlayerStatsType.Crystal, "Crystal: " + stats.Get(PlayerStatsType.Crystal) + " / " + stats.Get(PlayerStatsType.MaxCrystal));
            }
            else if (type == PlayerType.Opponent)
            {
                Opponent.Stats.SetText(PlayerStatsType.Hp, "HP: " + stats.Get(PlayerStatsType.Hp) + " / " + stats.Get(PlayerStatsType.MaxHp));
                Opponent.Stats.SetText(PlayerStatsType.Metal, "Metal: " + stats.Get(PlayerStatsType.Metal) + " / " + stats.Get(PlayerStatsType.MaxMetal));
                Opponent.Stats.SetText(PlayerStatsType.Deuterium, "Deuterium: " + stats.Get(PlayerStatsType.Deuterium) + " / " + stats.Get(PlayerStatsType.MaxDeuterium));
                Opponent.Stats.SetText(PlayerStatsType.Crystal, "Crystal: " + stats.Get(PlayerStatsType.Crystal) + " / " + stats.Get(PlayerStatsType.MaxCrystal));
            }
        }

        /// <summary>
        ///     Set a button if it is clickable.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="clickable"></param>
        public void SetButtonClickable(ButtonType type, bool clickable)
        {
            // TODO: Set button clickable
            if (type != ButtonType.NextPhaseButton) return;
            NextButton.interactable = clickable;
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
            var ownerController = owner == PlayerType.Player ? Player : Opponent;
            var card = Instantiate(Resources.Load(cardName)) as GameObject;
            _idDictionary.Add(id, card);
            if (card == null)
                return null;
            if (destination == ZoneType.Hand)
                ownerController.MoveToHand(card);
            else
                ownerController.MoveToBattlefield(card);
            return card.GetComponent<Card>();
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
            var card = _idDictionary[id];
            var ownerController = owner == PlayerType.Player ? Player : Opponent;
            if (destination == ZoneType.Hand)
                ownerController.MoveToHand(card);
            else
                ownerController.MoveToBattlefield(card);
        }

        /// <summary>
        ///     Set the text of Phase Text
        /// </summary>
        /// <param name="text"></param>
        public void SetPhaseText(string text)
        {
            // TODO: Set phase text
            PhaseText.text = text;
        }

        /// <summary>
        ///     Set the card IsFront property.
        /// </summary>
        /// <param name="id">Card id.</param>
        /// <param name="isFront"></param>
        public void SetCardIsFront(string id, bool isFront)
        {
            // TODO: Set card IsFront
            var card = _idDictionary[id];
            card.GetComponent<Card>().IsFront = isFront;
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
            ResourcePanelController.gameObject.SetActive(true);
            ResourcePanelController.MetalButton.interactable = metalEnable;
            ResourcePanelController.CrystalButton.interactable = crystalEnable;
            ResourcePanelController.DeuteriumButton.interactable = deuteriumEnable;
            ClearListener();
            ResourcePanelController.MetalButton.onClick.AddListener(() =>
            {
                onClose(ResourceType.Metal);
                OnButtonClick(this, new ButtonClickEventArgs(ButtonType.Undefined));
            });
            ResourcePanelController.CrystalButton.onClick.AddListener(() =>
            {
                onClose(ResourceType.Crystal);
                OnButtonClick(this, new ButtonClickEventArgs(ButtonType.Undefined));
            });
            ResourcePanelController.DeuteriumButton.onClick.AddListener(() =>
            {
                onClose(ResourceType.Deuterium);
                OnButtonClick(this, new ButtonClickEventArgs(ButtonType.Undefined));
            });
        }


        private void ClearListener()
        {
            ResourcePanelController.MetalButton.onClick.RemoveAllListeners();
            ResourcePanelController.CrystalButton.onClick.RemoveAllListeners();
            ResourcePanelController.DeuteriumButton.onClick.RemoveAllListeners();
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
            var selected = new List<string>();
            foreach (var t in idList)
            {
                AddButton(t, selected, allowCancel);
            }
            NextButton.onClick.AddListener(() =>
            {
                onClose(selected.ToArray());
                OnButtonClick(this, new ButtonClickEventArgs(ButtonType.NextPhaseButton));
            });
        }


        private void AddButton(string t, ICollection<string> selected, bool allowCancel)
        {
            var isClicked = false;
            var card = _idDictionary[t];
            var button = card.AddComponent<Button>();
            button.interactable = true;
            button.onClick.AddListener(() =>
            {
                if (!isClicked || !allowCancel)
                {
                    isClicked = true;
                    selected.Add(t);
                }
                else
                {
                    isClicked = false;
                    selected.Remove(t);
                }
            });
        }
        /// <summary>
        ///     Set a card to be draggable or not.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="draggable"></param>
        public void SetDraggable(string id, bool draggable)
        {
            // TODO: Set Draggable
            var card = _idDictionary[id];
            var drag = card.GetComponent<Draggable>();
            if (draggable && drag == null)
                card.AddComponent<Draggable>();
            if (!draggable && drag != null)
                Destroy(drag);
        }
    }
}