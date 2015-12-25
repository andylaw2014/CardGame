using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Utility
{
    public static class Extension
    {
        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
        {
            return
                (from Transform tr in parent.transform where tr.tag == tag select tr.GetComponent<T>()).FirstOrDefault();
        }

        public static GameObject FindChildWithTag(this GameObject parent, string tag)
        {
            return (from Transform tr in parent.transform where tr.tag == tag select tr.gameObject).FirstOrDefault();
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof (T)).Cast<T>();
        }

        public static Vector3 Add(this Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector3 Minus(this Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector3 Add(this Vector2 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v2.z);
        }

        public static void Swap<T>(ref T source, ref T target)
        {
            var temp = source;
            source = target;
            target = temp;
        }

        public static void Swap<T>(this IList<T> list, int source, int target)
        {
            var temp = list[source];
            list[source] = list[target];
            list[target] = temp;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var count = list.Count;
            while (count > 1)
            {
                var index = Random.Range(0, count);
                count--;
                list.Swap(index, count);
            }
        }

        public static PlayerType Oopponent(this PlayerType player)
        {
            return player == PlayerType.Player ? PlayerType.Player : PlayerType.Opponent;
        }

        public static void MoveToParent(this GameObject source, GameObject parent)
        {
            source.transform.SetParent(parent.transform);
            source.transform.localScale = Vector3.one;
        }
    }
}