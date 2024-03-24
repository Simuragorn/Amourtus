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
            Idle,
            Move,
            Attack_1,
            Attack_2,
            Attack_3,
            Hit,
            Death
        }
    }
}
