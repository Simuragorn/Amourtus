using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntruderMinionHealth : IntruderHealth
{
    protected IntruderMinion intruderMinion => character as IntruderMinion;
}
