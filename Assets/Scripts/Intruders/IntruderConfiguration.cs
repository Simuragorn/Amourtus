using UnityEngine;

public abstract class IntruderConfiguration : CharacterConfiguration
{
    [SerializeField] protected float maxMorale;

    public float MaxMorale => maxMorale;
}
