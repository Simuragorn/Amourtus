using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InsiderAttack : Attack
{
    protected override bool IsEnemy(Collider2D collider)
    {
        return collider.gameObject.CompareTag(TagConstants.Intruder);
    }
}
