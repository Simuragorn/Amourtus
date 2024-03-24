namespace Assets.Scripts.Core
{
    public class AnimationsModule : CharacterModule
    {
        public void MakeDamage()
        {
            character.MakeDamage();
        }
        public void FinishAttack()
        {
            character.FinishAttack();
        }
        public void HitRecovered()
        {
            character.HitRecovered();
        }
    }
}
