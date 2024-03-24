using UnityEngine;

public class IntruderAttack : Attack
{
    protected override bool IsEnemy(Collider2D collider)
    {
        var insider = collider.GetComponent<Insider>();
        return insider != null;
    }
}
