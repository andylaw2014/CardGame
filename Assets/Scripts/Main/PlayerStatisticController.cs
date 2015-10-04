using UnityEngine;
using UnityEngine.UI;

public class PlayerStatisticController : MonoBehaviour
{
    [HideInInspector]
    public int Hp;
    [HideInInspector]
    public int Mana1;
    [HideInInspector]
    public int Mana2;
    [HideInInspector]
    public int Mana3;

    public int MaxHp;
    public int MaxMana1;
    public int MaxMana2;
    public int MaxMana3;

    public Text HpText;
    public Text Mana1Text;
    public Text Mana2Text;
    public Text Mana3Text;

    void Start()
    {
        Hp = MaxHp;
        Mana1 = MaxMana1;
        Mana2 = MaxMana2;
        Mana2 = MaxMana3;
    }

    void OnGUI()
    {
        HpText.text = "HP: " + Hp + " / " + MaxHp;
        Mana1Text.text = "M1: " + Mana1 + " / " + MaxMana1;
        Mana2Text.text = "M2: " + Mana2 + " / " + MaxMana2;
        Mana3Text.text = "M3: " + Mana3 + " / " + MaxMana3;
    }
}
