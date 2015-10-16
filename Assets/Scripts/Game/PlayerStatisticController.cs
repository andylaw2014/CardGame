using UnityEngine;
using UnityEngine.UI;

public class PlayerStatisticController : MonoBehaviour
{
    public int MaxHp;
    public Text HpText;
    public Text MetalText;
    public Text CrystalText;
    public Text DeuteriumText;
    public Value<EnumType.Resource> Max;
    [HideInInspector]
    public int Hp;

    private Value<EnumType.Resource>  _resource;

    void Awake()
    {
        Hp = MaxHp;
        _resource= new Value<EnumType.Resource>();
        Max = new Value<EnumType.Resource>();
        Max[EnumType.Resource.Metal] = 0;
        Max[EnumType.Resource.Crystal] = 0;
        Max[EnumType.Resource.Deuterium] = 0;
        ResetResource();
    }

    void OnGUI()
    {
        HpText.text = "HP: " + Hp + " / " + MaxHp;
        MetalText.text = "Metal: " + _resource[EnumType.Resource.Metal] + " / " + Max[EnumType.Resource.Metal];
        CrystalText.text = "Crystal: " + _resource[EnumType.Resource.Crystal] + " / " + Max[EnumType.Resource.Crystal];
        DeuteriumText.text = "Deuterium: " + _resource[EnumType.Resource.Deuterium] + " / " + Max[EnumType.Resource.Deuterium];
    }

    public void ResetResource()
    {
        _resource[EnumType.Resource.Metal] = Max[EnumType.Resource.Metal];
        _resource[EnumType.Resource.Crystal] = Max[EnumType.Resource.Crystal];
        _resource[EnumType.Resource.Deuterium] = Max[EnumType.Resource.Deuterium];
    }

    public int this[EnumType.Resource i]
    {
        get { return _resource[i]; }
        set { _resource[i] = value; }
    }
}
