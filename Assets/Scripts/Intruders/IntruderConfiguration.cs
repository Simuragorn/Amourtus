using Assets.Scripts.Dto;
using UnityEngine;

public abstract class IntruderConfiguration : CharacterConfiguration
{
    [SerializeField] protected float maxMorale;
    [SerializeField] protected SoulTypeEnum soulType;
    [SerializeField] protected CoinTypeEnum coinType;
    public float MaxMorale => maxMorale;
    public SoulTypeEnum SoulType => soulType;
    public CoinTypeEnum CoinType => coinType;
}
