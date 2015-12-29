using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Gui.Controller
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject Battlefield;
        public GameObject Hand;
        public StatisticsController Stats;

        /// <summary>
        ///     Move card to hand.
        /// </summary>
        /// <param name="card">Card to move.</param>
        public void MoveToHand(GameObject card)
        {
            // TODO: Move card to hand
            card.MoveToParent(Hand);
            // Use MoveToParent
        }

        /// <summary>
        ///     Move card to Battlefield.
        /// </summary>
        /// <param name="card">Card to move.</param>
        public void MoveToBattlefield(GameObject card)
        {
            // TODO: Move card to Battlefield
            card.MoveToParent(Battlefield);
            // Use MoveToParent
        }
    }
}