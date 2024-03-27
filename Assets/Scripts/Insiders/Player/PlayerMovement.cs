using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : InsiderMovement
{
    [SerializeField] private Animator animator;
    protected override void Awake()
    {
        base.Awake();
        movementBlockingLayers = new List<string> { LayersConstants.Obstacle, LayersConstants.Intruder, LayersConstants.InsiderObstacle };
    }

    protected override void Update()
    {
        base.Update();
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        TryMove(horizontalAxis);
    }
}
