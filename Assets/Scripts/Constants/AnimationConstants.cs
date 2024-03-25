using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Constants
{
    public static class AnimationConstants
    {
        public const string AnimationStateKey = "state";
        public enum AnimationStateEnum
        {
            Idle = 0,
            Move = -1,
            TakeHit = -5,
            Death = -10,

            Attack_1 = 1,
            Attack_2 = 2,
            Attack_3 = 3,
            Attack_4 = 4,
        }
    }
}
