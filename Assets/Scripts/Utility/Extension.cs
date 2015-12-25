using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    }
}