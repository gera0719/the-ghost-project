using UnityEngine;
using GhostProject.Core;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [Header("Physics & Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PolygonCollider2D col;
    [SerializeField] private LayerMask groundLayer;

    private GhostProject.Core.Player playerLogic;

    private float horizontalInput;
    private bool isGrounded;
    private float baseScale = 0.4f;

    void Start()
    {
        playerLogic = new GhostProject.Core.Player();

        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (col == null) col = GetComponent<PolygonCollider2D>();

        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Crouch(true);
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            Crouch(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }


    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        playerLogic.Move();

        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        playerLogic.Jump();

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void Crouch(bool isCrouching)
    {
        playerLogic.Crouch();

        if (isCrouching)
        {
            transform.localScale = new Vector3(baseScale, baseScale * 0.5f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(baseScale, baseScale, 1f);
        }
    }

    private void Interact()
    {
        playerLogic.Interact();
        Debug.Log("[PlayerController] Interaction started!");
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, extraHeight, groundLayer);
        return hit.collider != null;
    }
}
