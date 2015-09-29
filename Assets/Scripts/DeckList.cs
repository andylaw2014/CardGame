using UnityEngine;
using System.Collections.Generic;

// Hold the decklist of player using.
public class DeckList : MonoBehaviour
{
    // Decklist storing the names of prefabs.
    public List<string> Deck;

    // Instance of DeckList object.
    private static DeckList me;
    
    void Start()
    {
        Deck = new List<string>();
    }
    
    void Awake()
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
