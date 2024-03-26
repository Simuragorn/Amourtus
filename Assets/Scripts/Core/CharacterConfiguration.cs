using Assets.Scripts.Dto;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterConfiguration : ScriptableObject
{
    [SerializeField] protected string CharacterName;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected List<AttackType> attackTypes = new List<AttackType>();

    public string Name => name;
    public float MaxHealth => maxHealth;
    public float MovementSpeed => movementSpeed;
    public List<AttackType> AttackTypes => attackTypes;
}
