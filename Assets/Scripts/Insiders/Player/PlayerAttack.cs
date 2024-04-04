using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : InsiderAttack
{
    protected override bool IsEnemy(Collider2D collider)
    {
        return collider.gameObject.CompareTag(TagConstants.Intruder);
    }

    protected override void Update()
    {
        reloadingTimeLeft = Mathf.Max(reloadingTimeLeft - Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            TryAttack();
        }
    }
}
