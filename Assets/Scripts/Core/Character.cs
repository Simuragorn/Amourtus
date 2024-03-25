using Assets.Scripts.Core;
using Assets.Scripts.Dto;
using System;
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

        movementModule.OnMovementStarted += MovementModule_OnMovementStarted;
        attackModule.OnAttackStarted += AttackModule_OnAttackStarted;
        healthModule.OnDeath += HealthModule_OnDeath;
        animationsModule.OnMakeDamage += AnimationsModule_OnMakeDamage;
        animationsModule.OnFinishAttack += AnimationsModule_OnFinishAttack;
        animationsModule.OnHitRecovered += AnimationsModule_OnHitRecovered;
    }

    private void MovementModule_OnMovementStarted(object sender, Vector2 translation)
    {
        transform.Translate(translation);
        var moveAnimation = translation == default ? AnimationStateEnum.Idle : AnimationStateEnum.Move;
        ChangeAnimationState(moveAnimation);
    }

    protected void AnimationsModule_OnHitRecovered(object sender, EventArgs e)
    {
        if (!isDead)
        {
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

    protected virtual void Update()
    {
        teleportationReloadingLeft = Mathf.Max(0, teleportationReloadingLeft - Time.deltaTime);
        teleportationAvailable = teleportationReloadingLeft <= 0;
    }

    protected void AttackModule_OnAttackStarted(object sender, AttackType e)
    {
        ChangeAnimationState(e.animationState);
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

    protected void HealthModule_OnDeath(object sender, EventArgs eventArgs)
    {
        isDead = true;
        ChangeAnimationState(AnimationStateEnum.Death);
        PauseModules(true);
    }

    public void GetHit(float damage)
    {
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
