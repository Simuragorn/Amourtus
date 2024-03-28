using Assets.Scripts.Core;
using Assets.Scripts.Dto;
using System;
using UnityEngine;
using static Assets.Scripts.Constants.AnimationConstants;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterConfiguration configuration;
    [SerializeField] protected float teleportationReloading = 1f;
    [SerializeField] protected float spawnVerticalOffset = 0f;

    protected AnimationStateEnum animationState = AnimationStateEnum.Idle;
    [SerializeField] protected Animator animator;

    [SerializeField] protected Health healthModule;
    [SerializeField] protected Movement movementModule;
    [SerializeField] protected Attack attackModule;
    [SerializeField] protected AnimationsModule animationsModule;

    public event EventHandler<Character> OnDeath;

    [SerializeField] protected bool isDead = false;
    protected float teleportationReloadingLeft;
    protected bool teleportationAvailable = true;
    protected bool isTeleportable = true;
    public bool TeleportationAvailable => teleportationAvailable;
    public AnimationStateEnum AnimationState => animationState;
    public CharacterConfiguration Configuration => configuration;
    protected Floor floor;
    [SerializeField] protected bool tookHit = false;

    protected virtual void Awake()
    {
        healthModule.Init(this);
        movementModule.Init(this);
        attackModule.Init(this);
        animationsModule.Init(this);

        movementModule.OnMovementStarted += MovementModule_OnMovementStarted;
        attackModule.OnAttackStarted += AttackModule_OnAttackStarted;
        healthModule.OnDeath += HealthModule_OnDeath;
        animationsModule.OnMakeDamage += AnimationsModule_OnMakeDamage;
        animationsModule.OnFinishAttack += AnimationsModule_OnFinishAttack;
        animationsModule.OnHitRecovered += AnimationsModule_OnHitRecovered;
    }

    protected virtual void Update()
    {
        if (isTeleportable)
        {
            teleportationReloadingLeft = Mathf.Max(0, teleportationReloadingLeft - Time.deltaTime);
            teleportationAvailable = teleportationReloadingLeft <= 0;
        }
    }

    protected void MovementModule_OnMovementStarted(object sender, Vector2 translation)
    {
        transform.Translate(translation);
        var moveAnimation = translation == default ? AnimationStateEnum.Idle : AnimationStateEnum.Move;
        ChangeAnimationState(moveAnimation);
    }

    protected void AnimationsModule_OnHitRecovered(object sender, EventArgs e)
    {
        if (!isDead)
        {
            tookHit = false;
            ChangeAnimationState(AnimationStateEnum.Idle);
            ResumeModules();
        }
    }

    protected void AnimationsModule_OnFinishAttack(object sender, EventArgs e)
    {
        attackModule.FinishAttack();
        ChangeAnimationState(AnimationStateEnum.Idle);
    }

    protected void AnimationsModule_OnMakeDamage(object sender, EventArgs e)
    {
        attackModule.MakeDamage();
    }

    public virtual void SetFloor(Floor currentFloor)
    {
        floor = currentFloor;
    }

    protected void AttackModule_OnAttackStarted(object sender, AttackType e)
    {
        ChangeAnimationState(e.AnimationState);
    }

    public bool CanAttack =>
        animationState == AnimationStateEnum.Idle || animationState == AnimationStateEnum.Move;

    public bool CanMove =>
        animationState == AnimationStateEnum.Idle || animationState == AnimationStateEnum.Move;

    public void Flip(bool flipX)
    {
        var localScale = transform.localScale;
        localScale.x = flipX ? -1 : 1;
        transform.localScale = localScale;
    }

    public virtual void Teleport(Teleport from, Teleport to)
    {
        if (isTeleportable && to != null)
        {
            Vector2 spawnPoint = to.SpawnPoint.position;
            teleportationAvailable = false;
            teleportationReloadingLeft = teleportationReloading;
            gameObject.transform.position = new Vector2(spawnPoint.x, spawnPoint.y + spawnVerticalOffset);
        }
    }

    protected virtual void HealthModule_OnDeath(object sender, EventArgs eventArgs)
    {
        isDead = true;
        PauseModules(true);
        ChangeAnimationState(AnimationStateEnum.Death);
        OnDeath?.Invoke(this, this);
    }

    public virtual void TakeHit(float damage)
    {
        if (!healthModule.IsPaused)
        {
            tookHit = true;
            healthModule.GetHit(damage);
            if (!isDead)
            {
                PauseModules(false);
                ChangeAnimationState(AnimationStateEnum.TakeHit);
            }
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
        if (IsNewAnimationStateValid(newAnimationState))
        {
            animationState = newAnimationState;
            animator.SetInteger(AnimationStateKey, (int)animationState);
        }
    }

    protected bool IsNewAnimationStateValid(AnimationStateEnum animationState)
    {
        if (isDead)
        {
            return animationState == AnimationStateEnum.Death;
        }
        if (tookHit)
        {
            return animationState == AnimationStateEnum.TakeHit;
        }
        return !isDead;
    }
}
