using UnityEngine;
using UnityEngine.UI;

public class CharaCard : Card
{
    public int HP;
    public int Attack;


    public override void OnPlay(Transform mTransform)
    {
        transform.SetParent(mTransform);
        Destroy(GetComponent<Draggable>());
    }

    public override bool IsPlayable()
    {
        if(player.Mana1>=Cost1&& player.Mana2 >= Cost2 && player.Mana3 >= Cost3)
        {
            player.Mana1 -= Cost1;
            player.Mana2 -= Cost2;
            player.Mana3 -= Cost3;
            return true;
        }
        return false;
    }

    public virtual void OnBlock()
    {

    }

    public virtual void OnAttack()
    {

    }

    public virtual void OnTurnEnd()
    {

    }

    public virtual void OnTurnStart()
    {

    }

    public virtual bool IsAttackable()
    {
        return true;
    }

    public virtual bool IsBlockable()
    {
        return true;
    }

    public virtual bool IsTagetable()
    {
        return true;
    }

}
