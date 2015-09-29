using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public int Cost1;
    public int Cost2;
    public int Cost3;
    protected Player player;

    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public virtual void OnPlay(Transform mTransform)
    {

    }

    public virtual bool IsPlayable()
    {
        return true;
    }

}
