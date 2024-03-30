using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float movementSpeed = 20f;

    [SerializeField] private bool followImmediately;
    [SerializeField] private bool followByX;
    [SerializeField] private bool followByY;
    void Update()
    {
        Vector2 direction = target.position - transform.position;
        Vector2 movement = movementSpeed * Time.deltaTime * direction;
        Vector2 newPosition = (Vector2)transform.position + movement;
        if (followImmediately)
        {
            newPosition = target.position;
        }
        newPosition.x = followByX ? newPosition.x : transform.position.x;
        newPosition.y = followByY ? newPosition.y : transform.position.y;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
