using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    private float horizontal;
    private float speed = 9f;
    [SerializeField] private float jumpingPower = 18f;
    private bool isFacingRight = true;
    [SerializeField] private Animator animator;
    private Rigidbody2D rigidbody2D;
    private CapsuleCollider2D playerCollider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask platformsLayer;
    [SerializeField] public GameObject currentGameObject;

    void Start()
    {
        rigidbody2D = PlayerManager.Instance.rigidbody2D;
        playerCollider = PlayerManager.Instance.playerCollider;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isAlive)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpingPower);
            }

            if (Input.GetButtonUp("Jump") && rigidbody2D.velocity.y > 0f)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.5f);
            }

            // If The player click "S" or "arrowDown" key and  if in circle area we detect the platform layer
            if (Input.GetAxisRaw("Vertical") < 0 && Physics2D.OverlapCircle(groundCheck.position, 0.2f, platformsLayer))
            {
                if (currentGameObject != null)
                {
                    string layerName = LayerMask.LayerToName(currentGameObject.layer);
                    if (layerName == "PlatformsLayer")
                    {
                        StartCoroutine(DisableCollision());
                    } 
                }
            }


            Animations();
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
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
        animator.SetFloat("velocityX", Mathf.Abs(rigidbody2D.velocity.x));
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checking if currently LayerMask is a PlatformsLayer
            currentGameObject = collision.gameObject;

     

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Checking if player exit from collider (platformLayer)
        // New implementation idea? Can we ue Physic2D.OverlapCircle instead this implementation?
            currentGameObject = null;

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
