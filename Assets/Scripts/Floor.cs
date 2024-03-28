using Assets.Scripts.Dto;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] protected Teleport floorStartTeleport;
    [SerializeField] protected Teleport floorEndTeleport;
    [SerializeField] protected Floor nextFloor;
    [SerializeField] protected Floor previousFloor;

    public Teleport FloorStartTeleport => floorStartTeleport;
    public Teleport FloorEndTeleport => floorEndTeleport;

    public event EventHandler<SoulTypeEnum> OnSoulGet;

    protected List<Insider> insiders = new List<Insider>();
    protected List<Intruder> intruders = new List<Intruder>();

    protected void Awake()
    {
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
        }
    }

    private void Intruder_OnDeath(object sender, Character e)
    {
        OnSoulGet?.Invoke(this, (e as Intruder).IntruderConfiguration.SoulType);
    }

    public void RemoveIntruder(Intruder intruder)
    {
        if (intruders.Contains(intruder))
        {
            intruders.Remove(intruder);
            intruder.OnDeath -= Intruder_OnDeath;
        }
    }

    public void AddInsider(Insider insider)
    {
        if (!insiders.Contains(insider))
        {
            insiders.Add(insider);
        }
    }

    public void RemoveInsider(Insider insider)
    {
        if (insiders.Contains(insider))
        {
            insiders.Remove(insider);
        }
    }
}
