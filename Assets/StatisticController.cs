using UnityEngine;
using UnityEngine.UI;

public class StatisticController : MonoBehaviour
{
    [HideInInspector]
    public int HP;
    [HideInInspector]
    public int Mana1;
    [HideInInspector]
    public int Mana2;
    [HideInInspector]
    public int Mana3;

    public int MaxHP;
    public int MaxMana1;
    public int MaxMana2;
    public int MaxMana3;

    public Text HPText;
    public Text Mana1Text;
    public Text Mana2Text;
    public Text Mana3Text;

    void Start()
    {
        HP = MaxHP;
        Mana1 = MaxMana1;
        Mana2 = MaxMana2;
        Mana2 = MaxMana3;
    }

    void OnGUI()
    {
        HPText.text = "HP: " + HP + " / " + MaxHP;
        Mana1Text.text = "M1: " + Mana1 + " / " + MaxMana1;
        Mana2Text.text = "M2: " + Mana2 + " / " + MaxMana2;
        Mana3Text.text = "M3: " + Mana3 + " / " + MaxMana3;
    }
}
