using Assets.Scripts.Constants;
using UnityEngine;

public class IntruderMinionAttack : IntruderAttack
{
    protected IntruderMinion intruderMinion => character as IntruderMinion;
}
