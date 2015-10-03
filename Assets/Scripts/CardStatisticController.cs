using UnityEngine;

public class CardStatisticController : MonoBehaviour
{
    [HideInInspector]
    public int HP;

    public int MaxHP;
    public int Attack;
    public int CostMana1;
    public int CostMana2;
    public int CostMana3;

    void Start()
    {
        HP = MaxHP;
    }
}
