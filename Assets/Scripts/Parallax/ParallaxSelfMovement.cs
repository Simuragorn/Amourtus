using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxSelfMovement : MonoBehaviour
{
    [SerializeField] private float xStartPoint;
    private float spriteWidth;
    private SpriteRenderer spriteRenderer;
    private Camera camera;
    [Range(-20f, 20f)]
    [SerializeField] private float parallaxMovementSpeed;
    private float latestCameraXPos;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        camera = Camera.main;
        xStartPoint = transform.position.x;
        spriteWidth = spriteRenderer.bounds.size.x;
    }
    void Update()
    {
        float cameraMovementDistance = camera.transform.position.x - latestCameraXPos;
        latestCameraXPos = camera.transform.position.x;
        float xTargetPosition = transform.position.x + cameraMovementDistance;
        xTargetPosition += parallaxMovementSpeed * Time.deltaTime;

        if (xTargetPosition > xStartPoint + spriteWidth)
        {
            xTargetPosition -= spriteWidth;
        }
        else if (xTargetPosition < xStartPoint - spriteWidth)
        {
            xTargetPosition += spriteWidth;
        }
        float distance = xTargetPosition - xStartPoint;
        transform.position = new Vector2(xStartPoint + distance, transform.position.y);
    }
}
