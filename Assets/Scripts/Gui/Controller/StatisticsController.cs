using Assets.Scripts.Core.Statistics;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Controller
{
    public class StatisticsController : MonoBehaviour
    {
        // TODO: Add references at inspector
        public Text CrystalText;
        public Text DeuteriumText;
        public Text HpText;
        public Text MetalText;
        public GameObject Parent;

        /// <summary>
        ///     Set the text of the text box.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="text"></param>
        public void SetText(PlayerStatsType type, string text)
        {
            // TODO: Set text
        }
    }
}