using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum GameStateEnum
{
    Battle,
    Keep,
    Defeat
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private DayPhaseManager dayPhaseManager;
    [SerializeField] private Crypt crypt;
    private Player player;
    public GameStateEnum State { get; private set; }
    public event EventHandler<GameStateEnum> OnStateChanged;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        player.OnDeath += Player_OnDeath;
        foreach (var floor in crypt.Floors)
        {
            floor.OnIntruderRemoved += Floor_OnIntruderRemoved;
        }
    }

    private void Player_OnDeath(object sender, Character e)
    {
        ChangeGameState(GameStateEnum.Defeat);
    }

    private void ChangeGameState(GameStateEnum newGameState)
    {
        State = newGameState;
        OnStateChanged?.Invoke(this, State);
    }

    private void Floor_OnIntruderRemoved(object sender, IntruderRemovingConditionEnum condition)
    {
        if (condition == IntruderRemovingConditionEnum.Retreat ||
            condition == IntruderRemovingConditionEnum.Death)
        {
            if (crypt.Floors.All(f => !f.Intruders.Any()) && State == GameStateEnum.Battle)
            {
                ChangeGameState(GameStateEnum.Keep);
            }
        }
    }

    void Start()
    {
        dayPhaseManager.OnDayPhaseChanged += DayPhaseManager_OnDayPhaseChanged;
    }

    private void DayPhaseManager_OnDayPhaseChanged(object sender, DayPhaseEnum e)
    {
        if (e == DayPhaseEnum.Day)
        {
            ChangeGameState(GameStateEnum.Battle);
        }
    }
}
