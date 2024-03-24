using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsiderAttack : Attack
{
    protected override bool IsEnemy(Collider2D collider)
    {
        var intruder = collider.GetComponent<Intruder>();
        return intruder != null;
    }
}
