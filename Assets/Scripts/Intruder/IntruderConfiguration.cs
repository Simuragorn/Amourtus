using UnityEngine;

[CreateAssetMenu(menuName = "Characters Configuration/Intruder")]
public class IntruderConfiguration : CharacterConfiguration
{
    [SerializeField] protected float maxMorale;

    public float MaxMorale => maxMorale;
}
