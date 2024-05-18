using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 9f;
    [SerializeField] private float jumpingPower = 18f;
    private bool isFacingRight = true;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask platformsLayer;

    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private GameObject currentGameObject;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // If The player click "S" or "arrowDown" key and  if in circle area we detect the platform layer
        if (Input.GetAxisRaw("Vertical") < 0 && Physics2D.OverlapCircle(groundCheck.position, 0.2f, platformsLayer))
        {
            if (currentGameObject != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
       

        Animations();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // Ground check if we can jump from GroundLayer/GroundLayer2/platformsLayer
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, (groundLayer | obstacleLayer | platformsLayer) );
    }

    private void Animations()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        animator.SetBool("grounded", IsGrounded());
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checking if currently LayerMask is a PlatformsLayer
        if (LayerMask.LayerToName(collision.gameObject.layer) == "PlatformsLayer")
        {
            currentGameObject = collision.gameObject;
        }
     

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Checking if player exit from collider (platformLayer)
        // New implementation idea? Can we ue Physic2D.OverlapCircle instead this implementation?
        if (LayerMask.LayerToName(collision.gameObject.layer) == "PlatformsLayer")
        {
            currentGameObject = null;
        }
    }

    // this function can be useful when we wanna Disable any collision
    private IEnumerator DisableCollision()
    {
        // getting platform collider
        // In the future there would be needed checking condition of the Layer
        CompositeCollider2D platformCollider = currentGameObject.GetComponent<CompositeCollider2D>();

        // Ignore collision
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        // wait a small amount of time
        yield return new WaitForSeconds(0.5f);
        // No longer ignore the collission
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);

    }


}
