using System.Collections.Generic;
using UnityEngine;

// Hold the decklist of player using.

namespace Assets.Scripts
{
    public class DeckList : MonoBehaviour
    {
        // Instance of DeckList object.
        private static DeckList me;
        // Decklist storing the names of prefabs.
        public List<string> Deck;

        private void Start()
        {
            Deck = new List<string>();
        }

        private void Awake()
        {
            // New level will not destory this object.
            DontDestroyOnLoad(gameObject);

            // Ensure only one DeckList object.
            if (me == null)
                me = this;
            else
                DestroyImmediate(gameObject);
        }
    }
}