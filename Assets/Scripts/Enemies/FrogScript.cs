using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : Singleton<FrogScript>
{
    public Rigidbody2D frogRigidBody2D;
    public Animator frogAnimator;
    private bool isFacingRight = true;
    public Vector2 rightSpriteOffset; // offset right
    public Vector2 leftSpriteOffset;  // offset left

    [SerializeField] private float frogSpeedHorizontal = 2f;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(move());
        isFacingRight = !isFacingRight;

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator move()
    {
        yield return new WaitForSeconds(0.5f);
        frogAnimator.SetBool("isMoving", true);
        frogRigidBody2D.velocity = new Vector2(frogSpeedHorizontal, 0f);
    }

    public IEnumerator Bounce()
    {
        // Changing the animation from  moving to Idle
        frogRigidBody2D.velocity = new Vector2(0, 0);
        frogAnimator.SetBool("isMoving", false);

        // Attack Animation
        yield return new WaitForSeconds(0.2f);
        frogAnimator.SetTrigger("Attack");

        // Reverse velocity and positionof the frog
        yield return new WaitForSeconds(2.0f);
        transform.Rotate(0f, 180f, 0f);


        // ----- 
        // The imitated solution to make offset to sprite to make it more realistic 
        // for now in unity for the leftSpriteOffSet is set 1.8 and -1.8 for right side
        Vector3 thePosition = transform.localPosition;

        if (isFacingRight)
        {
            thePosition += (Vector3)leftSpriteOffset; // Change of the position by the offset
        }
        else
        {
            thePosition += (Vector3)rightSpriteOffset; // Change of the position by the offset
        }
        transform.localPosition = thePosition;

        // ----

        isFacingRight = !isFacingRight;

        yield return new WaitForSeconds(0.8f);
        frogAnimator.SetBool("isMoving", true);
        yield return new WaitForSeconds(0.6f);
        frogRigidBody2D.velocity = new Vector2(frogSpeedHorizontal *= -1f, 0);
        

    }



}