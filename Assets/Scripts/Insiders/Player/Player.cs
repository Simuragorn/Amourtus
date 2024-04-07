using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Insider
{
    [SerializeField] private Floor startingFloor;
    protected PlayerConfiguration PlayerConfiguration => Configuration as PlayerConfiguration;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        transform.position = startingFloor.FloorEndTeleport.SpawnPoint.position;
        SetFloor(startingFloor);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            TryUseTeleport();
        }
    }
}
