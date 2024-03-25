using System;

namespace Assets.Scripts.Core
{
    public class AnimationsModule : CharacterModule
    {
        public event EventHandler OnMakeDamage;
        public event EventHandler OnFinishAttack;
        public event EventHandler OnHitRecovered;
        public void MakeDamage()
        {
            OnMakeDamage?.Invoke(this, EventArgs.Empty);
        }
        public void FinishAttack()
        {
            OnFinishAttack?.Invoke(this, EventArgs.Empty);
        }
        public void HitRecovered()
        {
            OnHitRecovered?.Invoke(this, EventArgs.Empty);
        }
    }
}
