using UnityEngine;
using UnityEngine.UI;

public class PlayerStatisticController : MonoBehaviour
{
    private Value<EnumType.Resource> _resource;
    public Text CrystalText;
    public Text DeuteriumText;

    [HideInInspector] public int Hp;

    public Text HpText;
    public Value<EnumType.Resource> Max;
    public int MaxHp;
    public Text MetalText;

    public int this[EnumType.Resource i]
    {
        get { return _resource[i]; }
        set { _resource[i] = value; }
    }

    private void Awake()
    {
        Hp = MaxHp;
        _resource = new Value<EnumType.Resource>();
        Max = new Value<EnumType.Resource>();
        Max[EnumType.Resource.Metal] = 0;
        Max[EnumType.Resource.Crystal] = 0;
        Max[EnumType.Resource.Deuterium] = 0;
        ResetResource();
    }

    private void OnGUI()
    {
        HpText.text = "HP: " + Hp + " / " + MaxHp;
        MetalText.text = "Metal: " + _resource[EnumType.Resource.Metal] + " / " + Max[EnumType.Resource.Metal];
        CrystalText.text = "Crystal: " + _resource[EnumType.Resource.Crystal] + " / " + Max[EnumType.Resource.Crystal];
        DeuteriumText.text = "Deuterium: " + _resource[EnumType.Resource.Deuterium] + " / " +
                             Max[EnumType.Resource.Deuterium];
    }

    public void ResetResource()
    {
        _resource[EnumType.Resource.Metal] = Max[EnumType.Resource.Metal];
        _resource[EnumType.Resource.Crystal] = Max[EnumType.Resource.Crystal];
        _resource[EnumType.Resource.Deuterium] = Max[EnumType.Resource.Deuterium];
    }
}