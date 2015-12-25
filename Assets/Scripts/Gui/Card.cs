using System;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Statistics;
using Assets.Scripts.Metadata;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
    public class Card : MonoBehaviour
    {
        private Image _cardImage;
        private CardStats _cardStats;
        private GuiMediator _guiMediator;

        private string _id = "";
        private bool _isFront;

        public Sprite Image;
        public Statistics Stats; // For grouping in inspector

        public bool IsFront
        {
            set
            {
                if (_isFront == value) return;
                _isFront = value;
                _cardImage.sprite = _isFront ? Image : _guiMediator.CardBack;
            }
        }

        public PlayerType Parent { get; set; }
        public ZoneType Zone { get; set; }

        /// <summary>
        ///     Card id.
        /// </summary>
        public string Id
        {
            get { return _id; }
            set
            {
                if (value != null && _id == "")
                    _id = value;
            }
        }

        /// <summary>
        ///     Use this for initialization
        /// </summary>
        private void Awake()
        {
            _cardStats = new CardStats();
            InitialStats();
            _guiMediator = GameObject.FindGameObjectWithTag(Tag.GuiMediator).GetComponent<GuiMediator>();
            _cardImage = GetComponent<Image>();
        }

        /// <summary>
        ///     Change the card view image when mouse enter a card.
        /// </summary>
        /// <param name="eventData"></param>
        private void OnPointerEnter(PointerEventData eventData)
        {
            if (_isFront)
                _guiMediator.CardView.sprite = Image;
        }

        /// <summary>
        ///     Initialization of statistics.
        /// </summary>
        private void InitialStats()
        {
            SetStats(CardStatsType.Hp, Stats.Hp);
            SetStats(CardStatsType.Atk, Stats.Atk);
            SetStats(CardStatsType.Metal, Stats.Metal);
            SetStats(CardStatsType.Crystal, Stats.Crystal);
            SetStats(CardStatsType.Deuterium, Stats.Deuterium);
        }

        private void UpdateInspectorStats(CardStatsType type, int value)
        {
            switch (type)
            {
                case CardStatsType.Hp:
                    Stats.Hp = value;
                    break;
                case CardStatsType.Atk:
                    Stats.Atk = value;
                    break;
                case CardStatsType.Metal:
                    Stats.Metal = value;
                    break;
                case CardStatsType.Crystal:
                    Stats.Crystal = value;
                    break;
                case CardStatsType.Deuterium:
                    Stats.Deuterium = value;
                    break;
            }
        }

        public void SetStats(CardStatsType type, int value)
        {
            _cardStats[type] = value;
            UpdateInspectorStats(type, value);
        }

        public int GetStats(CardStatsType type)
        {
            return _cardStats[type];
        }

        /// <summary>
        ///     For initial value and inspector view only.
        /// </summary>
        [Serializable]
        public class Statistics
        {
            public int Hp, Atk, Metal, Crystal, Deuterium;
        }
    }
}