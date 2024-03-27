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
        }
    }

    public void RemoveIntruder(Intruder intruder)
    {
        if (intruders.Contains(intruder))
        {
            intruders.Remove(intruder);
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
