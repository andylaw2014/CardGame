using UnityEngine;

namespace Assets.Scripts.Outdate.UI
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject HandZone;
        public GameObject MinionZone;

        public void AddToHand(GameObject card)
        {
            AddToParent(card, HandZone);
        }

        public void AddToMinion(GameObject card)
        {
            AddToParent(card, MinionZone);
        }

        private static void AddToParent(GameObject obj, GameObject parent)
        {
            obj.transform.SetParent(parent.transform);
            obj.transform.localScale = Vector3.one;
        }
    }
}