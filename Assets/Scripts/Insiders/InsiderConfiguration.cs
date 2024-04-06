using Assets.Scripts.Dto;
using UnityEngine;

public abstract class InsiderConfiguration : CharacterConfiguration
{
    [SerializeField] private SoulTypeEnum soulType;
    [SerializeField] protected int cost;

    public SoulTypeEnum SoulType => soulType;
    public int Cost => cost;
}
