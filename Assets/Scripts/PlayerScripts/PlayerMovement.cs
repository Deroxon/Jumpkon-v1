using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    private float horizontal;
    private float speed = 9f;
    [SerializeField] private float jumpingPower = 18.5f;
    private bool isFacingRight = true;
    [SerializeField] public Animator animator;
    private Rigidbody2D playerRigidbody2D;
    private CapsuleCollider2D playerCollider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask platformsLayer;
    [SerializeField] private LayerMask hayLayer;
    [SerializeField] private LayerMask movingPlatformsLayer;
    [SerializeField] public GameObject currentGameObject;
    [SerializeField] private bool isHit;
    private Rigidbody2D platformRigidbody2D;

    public ParticleSystem dust;

    void Start()
    {
        playerRigidbody2D = PlayerManager.Instance.playerRigidbody2D;
        playerCollider = PlayerManager.Instance.playerCollider;
    }



    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isAlive && !GameManager.Instance.isPaused)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpingPower);
                AudioManager.Instance.PlaySFX("Jump");
                dust.Play();
            }

            if (Input.GetButtonUp("Jump") && playerRigidbody2D.velocity.y > 0f)
            {
                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, playerRigidbody2D.velocity.y * 0.5f);
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
        Movement();
    }

    // Ground check if we can jump from GroundLayer/GroundLayer2/platformsLayer
    private bool IsGrounded()
    {
        PlayerManager.Instance.CountFalled = 0;
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, (groundLayer | obstacleLayer | platformsLayer | movingPlatformsLayer | hayLayer ) );
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
        if(IsGrounded())
        {
            dust.Play();
        }

        animator.SetBool("grounded", IsGrounded());
        animator.SetInteger("horizontal", Mathf.Abs((int) horizontal));
    }

    //Checking if platform player is standing on is moving and adjusting velocity accordingly.
    private void Movement()
    {
        if (PlatformCheck.Instance.currentPlatform != null)
        {
            platformRigidbody2D = PlatformCheck.Instance.currentPlatform.GetComponent<Rigidbody2D>();
            playerRigidbody2D.velocity = new Vector2(horizontal * speed + platformRigidbody2D.velocity.x, playerRigidbody2D.velocity.y);
        }
        else
            playerRigidbody2D.velocity = new Vector2(horizontal * speed, playerRigidbody2D.velocity.y);
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
        Physics2D.IgnoreCollision(gameObject.GetComponentInChildren<CircleCollider2D>(), platformCollider);
        // wait a small amount of time
        yield return new WaitForSeconds(0.3f);
        // No longer ignore the collission
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        Physics2D.IgnoreCollision(gameObject.GetComponentInChildren<CircleCollider2D>(), platformCollider, false);

    }

  
    public IEnumerator AnimationDamage()
    {
        animator.SetBool("isHitted", true);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("isHitted", false);

    }


}
