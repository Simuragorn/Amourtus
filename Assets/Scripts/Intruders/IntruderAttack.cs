using Assets.Scripts.Constants;
using UnityEngine;

public abstract class IntruderAttack : Attack
{
    protected override bool IsEnemy(Collider2D collider)
    {
        return collider.gameObject.CompareTag(TagConstants.Insider) || collider.gameObject.CompareTag(TagConstants.Player);
    }
}
