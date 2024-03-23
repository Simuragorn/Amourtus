using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float runningSpeed = 8f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private bool isRunning;

    const string isRunningAnimatorKey = "isRunning";

    void Update()
    {

        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        if (horizontalAxis != 0)
        {
            spriteRenderer.flipX = horizontalAxis < 0;
        }
        isRunning = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool(isRunningAnimatorKey, isRunning);
        var direction = new Vector2(horizontalAxis, 0);
        float speed = isRunning ? runningSpeed : movementSpeed;
        transform.Translate(speed * Time.deltaTime * direction);
    }
}
