using System;
using UnityEngine;

namespace Assets.Scripts.Core.Card
{
    public class Card : MonoBehaviour
    {
        private CardStats _cardStats;

        /// <summary>
        ///     For initial value and inspector view only.
        /// </summary>
        public int Hp, Atk, Metal, Crystal, Deuterium;
        

        // Use this for initialization
        private void Start()
        {
            _cardStats = new CardStats();
            InitialStats();
        }

        private void InitialStats()
        {
            SetStats(CardStatsType.Hp, Hp);
            SetStats(CardStatsType.Atk, Atk);
            SetStats(CardStatsType.Metal, Metal);
            SetStats(CardStatsType.Crystal, Crystal);
            SetStats(CardStatsType.Deuterium, Deuterium);
        }

        private void UpdateInspectorStats(CardStatsType type, int value)
        {
            switch (type)
            {
                case CardStatsType.Hp:
                    Hp = value;
                    break;
                case CardStatsType.Atk:
                    Atk = value;
                    break;
                case CardStatsType.Metal:
                    Metal = value;
                    break;
                case CardStatsType.Crystal:
                    Crystal = value;
                    break;
                case CardStatsType.Deuterium:
                    Deuterium = value;
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
    }
}