using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;

public class IntruderMinionMovement : IntruderMovement
{
    protected IntruderMinion intruderMinion => character as IntruderMinion;
}
