using Assets.Scripts.Dto;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum IntruderRemovingConditionEnum
{
    FloorChange,
    Death,
    Retreat
}

public enum FloorTypeEnum
{
    Entrance,
    Common,
    Throne
}
public class Floor : MonoBehaviour
{
    [SerializeField] protected Teleport floorStartTeleport;
    [SerializeField] protected Teleport floorEndTeleport;
    [SerializeField] protected string name;
    [SerializeField] protected CoinTypeEnum coinType;
    [SerializeField] protected int cost;

    public event EventHandler<IntruderRemovingConditionEnum> OnIntruderRemoved;
    public event EventHandler<SoulTypeEnum> OnSoulGet;
    public event EventHandler<CoinTypeEnum> OnCoinGet;
    public event EventHandler OnFloorUpdated;

    public bool IsPurchased;
    public Teleport FloorStartTeleport => floorStartTeleport;
    public Teleport FloorEndTeleport => floorEndTeleport;
    public string Name => name;
    public IReadOnlyList<Insider> Insiders => insiders;
    public IReadOnlyList<Intruder> Intruders => intruders;
    public Crypt Crypt => crypt;

    protected List<Insider> insiders = new List<Insider>();
    protected List<Intruder> intruders = new List<Intruder>();
    protected Crypt crypt;
    public FloorTypeEnum Type => type;
    protected FloorTypeEnum type;

    protected Floor nextFloor;
    protected Floor previousFloor;

    private const float FloorsVerticalOffset = 25;

    public void Init(Crypt cryptObject, Floor previousFloorObject, Floor nextFloorObject, FloorTypeEnum floorType)
    {
        crypt = cryptObject;
        previousFloor = previousFloorObject;
        nextFloor = nextFloorObject;
        type = floorType;

        floorStartTeleport.Init(this, TeleportTypeEnum.Start);
        floorEndTeleport.Init(this, TeleportTypeEnum.End);
        if (previousFloor != null)
        {
            floorStartTeleport.ConnectedTeleport = previousFloor.floorEndTeleport;
            transform.position = new Vector2(previousFloor.transform.position.x, previousFloor.transform.position.y - FloorsVerticalOffset);
        }
        if (nextFloor != null)
        {
            floorEndTeleport.ConnectedTeleport = nextFloor.floorStartTeleport;
        }

        if (floorType != FloorTypeEnum.Common)
        {
            IsPurchased = true;
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
        OnCoinGet?.Invoke(this, (e as Intruder).IntruderConfiguration.CoinType);
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
