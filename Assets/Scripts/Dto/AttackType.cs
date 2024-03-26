using Assets.Scripts.Constants;
using System;

namespace Assets.Scripts.Dto
{
    [Serializable]
    public class AttackType
    {
        public AnimationConstants.AnimationStateEnum animationState;
        public string description;
        public float reloading;
        public float damage;
    }
}
