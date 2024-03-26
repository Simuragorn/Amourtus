using UnityEngine;

namespace Assets.Scripts.Core
{
    public abstract class CharacterModule : MonoBehaviour
    {
        [SerializeField] protected bool isPaused = false;
        protected Character character;
        public bool IsPaused => isPaused;
        public virtual void Init(Character currentCharacter)
        {
            character = currentCharacter;
        }

        public virtual void Pause(bool disablePhysics)
        {
            isPaused = true;
        }

        public virtual void Resume()
        {
            isPaused = false;
        }
    }
}
