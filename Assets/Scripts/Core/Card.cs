using UnityEngine;
using Core.Zone;

namespace Core
{
    public class Card : MonoBehaviour
    {
        public Player Owner { get; set; }
        public OrderedZone Zone { get; private set; }

        public void ChangeZone(OrderedZone zone)
        {
            Zone = zone;
        }
    }
}