using UnityEngine;
using UnityEngine.UI;

public class CardStatisticController : MonoBehaviour
{
    [HideInInspector]
    public int HP;

    public int MaxHP;
    public int Attack;
    public int CostMana1;
    public int CostMana2;
    public int CostMana3;

    public Text HPText;
    public Text Mana1Text;
    public Text Mana2Text;
    public Text Mana3Text;

    void Start()
    {
        HP = MaxHP;
    }
}
