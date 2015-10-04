using UnityEngine;

public class CardStatisticController : MonoBehaviour
{
    [HideInInspector]
    public int Hp;

    public int MaxHp;
    public int Attack;
    public int CostMana1;
    public int CostMana2;
    public int CostMana3;

    void Start()
    {
        Hp = MaxHp;
    }
}
