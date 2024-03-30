using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxCameraMovement : MonoBehaviour
{
    private float xStartPoint;
    private float spriteWidth;
    private SpriteRenderer spriteRenderer;
    private Camera camera;
    [SerializeField] private float parallaxMultiplier;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        camera = Camera.main;
        xStartPoint = transform.position.x;
        spriteWidth = spriteRenderer.bounds.size.x;
    }
    void Update()
    {
        float targetXPos = camera.transform.position.x * (1 - parallaxMultiplier);
        float distance = camera.transform.position.x * parallaxMultiplier;

        transform.position = new Vector2(xStartPoint + distance, transform.position.y);
        if (targetXPos > xStartPoint + spriteWidth)
        {
            xStartPoint += spriteWidth;
        }
        else if (targetXPos < xStartPoint - spriteWidth)
        {
            xStartPoint -= spriteWidth;
        }
    }
}
