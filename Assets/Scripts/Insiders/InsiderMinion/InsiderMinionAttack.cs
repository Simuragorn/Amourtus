using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InsiderMinionAttack : InsiderAttack
{
    protected InsiderMinion insiderMinion => character as InsiderMinion;
}