using Assets.Scripts.Constants;
using System;

namespace Assets.Scripts.Dto
{
    [Serializable]
    public class AttackType
    {
        public AnimationConstants.AnimationStateEnum AnimationState;
        public string Description;
        public float Reloading;
        public float Damage;
    }
}
