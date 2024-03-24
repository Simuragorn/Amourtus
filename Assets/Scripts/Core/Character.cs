using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Assets.Scripts.Constants.AnimationConstants;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private float teleportationReloading = 1f;
    [SerializeField] private float spawnVerticalOffset = 0f;

    protected AnimationStateEnum animationState = AnimationStateEnum.Idle;
    [SerializeField] private Animator animator;

    [SerializeField] private Health healthModule;
    [SerializeField] private Movement movementModule;
    [SerializeField] private Attack attackModule;
    [SerializeField] private AnimationsModule animationsModule;

    [SerializeField] private bool isDead = false;
    private float teleportationReloadingLeft;
    private bool teleportationAvailable = true;
    public bool TeleportationAvailable => teleportationAvailable;
    public AnimationStateEnum AnimationState => animationState;

    protected void Awake()
    {
        movementModule.Init(this);
        attackModule.Init(this);
        healthModule.Init(this);
        animationsModule.Init(this);
    }

    public void Flip(bool flipX)
    {
        var localScale = transform.localScale;
        localScale.x = flipX ? -1 : 1;
        transform.localScale = localScale;
    }

    public virtual void TeleportTo(Teleport teleport)
    {
        Vector2 spawnPoint = teleport.SpawnPoint.position;
        teleportationAvailable = false;
        teleportationReloadingLeft = teleportationReloading;
        gameObject.transform.position = new Vector2(spawnPoint.x, spawnPoint.y + spawnVerticalOffset);
    }

    protected virtual void Update()
    {
        teleportationReloadingLeft = Mathf.Max(0, teleportationReloadingLeft - Time.deltaTime);
        teleportationAvailable = teleportationReloadingLeft <= 0;
    }

    public void ChangeAttackState(AnimationStateEnum attackAnimationState)
    {
        ChangeAnimationState(attackAnimationState);
    }

    public void MakeDamage()
    {
        attackModule.MakeDamage();
    }

    public void FinishAttack()
    {
        attackModule.FinishAttack();
        ChangeAnimationState(AnimationStateEnum.Idle);
    }

    public void ChangeMoveState(AnimationStateEnum movementAnimationState)
    {
        ChangeAnimationState(movementAnimationState);
    }

    public void StartDeath()
    {
        isDead = true;
        ChangeAnimationState(AnimationStateEnum.Death);
        PauseModules(true);
    }

    public void GetHit(float damage)
    {
        Debug.Log("Hit");
        if (!healthModule.IsPaused)
        {
            healthModule.GetHit(damage);
            if (!isDead)
            {
                ChangeAnimationState(AnimationStateEnum.Hit);
                PauseModules(false);
            }
        }
    }

    public void HitRecovered()
    {
        Debug.Log("Recover");
        if (!isDead)
        {
            ChangeMoveState(AnimationStateEnum.Idle);
            ResumeModules();
        }
    }

    protected void PauseModules(bool disablePhysics)
    {
        healthModule.Pause(disablePhysics);
        attackModule.Pause(disablePhysics);
        movementModule.Pause(disablePhysics);
    }

    protected void ResumeModules()
    {
        movementModule.Resume();
        healthModule.Resume();
        attackModule.Resume();        
    }

    protected void ChangeAnimationState(AnimationStateEnum newAnimationState)
    {
        animationState = newAnimationState;
        animator.SetInteger(AnimationStateKey, (int)animationState);
    }
}
