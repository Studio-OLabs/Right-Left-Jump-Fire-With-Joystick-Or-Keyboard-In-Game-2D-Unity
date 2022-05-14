using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



    public class Player : MonoBehaviour
 {
    [Space]
    [Header("Stats")]
    public float speed = 10f;
    public float jumpforce = 50f;
    public Rigidbody2D rb;

    [Space]
    [Header("RayCast")]
    public Vector2 bottomRightOffset, bottomLeftOffset;
    public LayerMask groundLayer;
    public float collisionRadius = 0.25f;

    [Space]
    [Header("Booleans")]
    public bool jumped = false;
    public bool isfire = false;
    public bool onGround;

    private float inputX;

    private Vector2 direction;

    private float xVelocity;
        // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

        // Update is called once per frame
    void Update()
    {
        direction = new Vector2(inputX, 0f);
        OnGround();

    }
    private void FixedUpdate()
    {
        Walk(direction);
        if (onGround && jumped)
        {
            Jump();
        }
        
    }

    private void Walk(Vector2 dir)
    {
        xVelocity = dir.normalized.x * speed;
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }
    private void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
    }
    private void Stop()
    {
        rb.velocity = Vector2.zero;
    } 

    public void MoveInput(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }
    public void JumpInputContext(InputAction.CallbackContext callbackContext)
    {
        if (!jumped)
            jumped = true;
    }
    public void FireInputContex(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && isfire == false)
        {
            Stop();
            Debug.Log("ATIRANDO");
        }
    }

    private void OnGround()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomRightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + bottomLeftOffset, collisionRadius, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomRightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomLeftOffset, collisionRadius);
    }
}