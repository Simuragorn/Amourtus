using Assets.Scripts.Dto;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public enum IntruderRemovingConditionEnum
{
    FloorChange,
    Death,
    Retreat
}
public class Floor : MonoBehaviour
{
    [SerializeField] protected Teleport floorStartTeleport;
    [SerializeField] protected Teleport floorEndTeleport;
    [SerializeField] protected Floor nextFloor;
    [SerializeField] protected Floor previousFloor;
    [SerializeField] protected string name;

    public Teleport FloorStartTeleport => floorStartTeleport;
    public Teleport FloorEndTeleport => floorEndTeleport;
    public string Name => name;
    public IReadOnlyList<Insider> Insiders => insiders;
    public IReadOnlyList<Intruder> Intruders => intruders;
    public Crypt Crypt => crypt;

    public event EventHandler<IntruderRemovingConditionEnum> OnIntruderRemoved;
    public event EventHandler<SoulTypeEnum> OnSoulGet;
    public event EventHandler OnFloorUpdated;

    protected List<Insider> insiders = new List<Insider>();
    protected List<Intruder> intruders = new List<Intruder>();

    private Crypt crypt;

    protected void Awake()
    {
        crypt = FindAnyObjectByType<Crypt>();
        floorStartTeleport.Init(this, TeleportTypeEnum.Start);
        floorEndTeleport.Init(this, TeleportTypeEnum.End);
        if (previousFloor != null)
        {
            floorStartTeleport.ConnectedTeleport = previousFloor.floorEndTeleport;
        }
        if (nextFloor != null)
        {
            floorEndTeleport.ConnectedTeleport = nextFloor.floorStartTeleport;
        }
    }

    public void AddIntruder(Intruder intruder)
    {
        if (!intruders.Contains(intruder))
        {
            intruders.Add(intruder);
            intruder.OnDeath += Intruder_OnDeath;
            OnFloorUpdated?.Invoke(this, EventArgs.Empty);
            Debug.Log("Floor updated");
        }
    }

    private void Intruder_OnDeath(object sender, Character e)
    {
        OnSoulGet?.Invoke(this, (e as Intruder).IntruderConfiguration.SoulType);
    }

    public void RemoveIntruder(Intruder intruder, IntruderRemovingConditionEnum removingCondition)
    {
        if (intruders.Contains(intruder))
        {
            intruders.Remove(intruder);
            intruder.OnDeath -= Intruder_OnDeath;
            OnFloorUpdated?.Invoke(this, EventArgs.Empty);
            OnIntruderRemoved?.Invoke(this, removingCondition);
        }
    }

    public void AddInsider(Insider insider)
    {
        if (!insiders.Contains(insider))
        {
            insiders.Add(insider);
            insider.OnDeath += Insider_OnDeath;
            OnFloorUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    public void RemoveInsider(Insider insider)
    {
        if (insiders.Contains(insider))
        {
            insiders.Remove(insider);
            insider.OnDeath -= Insider_OnDeath;
            OnFloorUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Insider_OnDeath(object sender, Character e)
    {

    }
}
