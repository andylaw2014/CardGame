using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public GameObject handZone;
    public GameObject MinionZone;
    public int MaxHP;
    public int MaxMana1;
    public int MaxMana2;
    public int MaxMana3;

    public Text HPText;
    public Text Mana1Text;
    public Text Mana2Text;
    public Text Mana3Text;
    [HideInInspector]
    public int HP;
    [HideInInspector]
    public int Mana1;
    [HideInInspector]
    public int Mana2;
    [HideInInspector]
    public int Mana3;

    private List<Card> hands;


    void Start()
    {
        hands = new List<Card>();
        HP = MaxHP;
        RestoreMana();
    }

    public void RestoreMana()
    {
        Mana1 = MaxMana1;
        Mana2 = MaxMana2;
        Mana3 = MaxMana3;
    }
    
    public void AddHand(Card card)
    {
        hands.Add(card);
        card.gameObject.transform.SetParent(handZone.transform);
    }

    void OnGUI()
    {
        HPText.text = HP + " / " + MaxHP;
        Mana1Text.text = Mana1 + " / " + MaxMana1;
        Mana2Text.text = Mana2 + " / " + MaxMana2;
        Mana3Text.text = Mana3 + " / " + MaxMana3;
    }

}
