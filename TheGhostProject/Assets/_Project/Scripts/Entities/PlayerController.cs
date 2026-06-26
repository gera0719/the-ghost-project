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
    private float baseScale = 3.48f;
    private IInteractable currentInteractable;

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
            if (transform.localScale.y > baseScale * 0.9f)
            {
                float fullHeight = col.bounds.size.y;

                transform.localScale = new Vector3(baseScale, baseScale * 0.5f, 1f);

                transform.position -= new Vector3(0f, fullHeight * 0.25f, 0f);
            }
        }
        else
        {
            if (transform.localScale.y < baseScale)
            {
                transform.localScale = new Vector3(baseScale, baseScale, 1f);

                float fullHeight = col.bounds.size.y;

                transform.position += new Vector3(0f, fullHeight * 0.25f, 0f);
            }
        }
    }

    private void Interact()
    {
        Debug.Log("[PlayerController] Interaction started!");
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
        else
        {
            Debug.LogWarning("[PlayerController] No interactable object nearby.");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("$\"<color=cyan>[Player] Interaction available: Pres E");

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<IInteractable>() == currentInteractable)
        {
            currentInteractable = null;
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, extraHeight, groundLayer);
        return hit.collider != null;
    }
}
