using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyLogicScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D EnemyRigidBody2D;
    [SerializeField]
    private Animator EnemyAnimator;

    private bool isFacingRight = true;
    public Vector2 rightSpriteOffset; // offset right
    public Vector2 leftSpriteOffset;  // offset left

    private EnemyStructure currentEnemy;
    private bool isEnemyGetted = false;

    public GameObject cannonBall;

    // Start is called before the first frame update
    void Start()
    {
       

        if (!isEnemyGetted)
        {
            StartCoroutine(loadEnemy());
            
            isFacingRight = !isFacingRight;
            
        }
    }


    private void Update()
    {
    }

    IEnumerator loadEnemy()
    {
        // get Animator
        EnemyAnimator = gameObject.GetComponent<Animator>();
        // get rigidBody
        EnemyRigidBody2D = gameObject.GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(0.4f);
        // find the Enemy by Current tag
        EnemyStructure foundEnemy = GameManager.Instance.enemiesList.Find(enemy => enemy.Tag == gameObject.tag);
        if(foundEnemy  != null)
        {
            currentEnemy = new EnemyStructure(foundEnemy); // Skopiowanie obiektu
        }

        if (currentEnemy.Tag != null)
        {
            yield return StartCoroutine(move(currentEnemy.OffsetMoveAnimation, currentEnemy.MoveVelocity));
            Debug.Log("Enemy loaded Correctly");
            
        }
        else
        {
            throw new System.Exception("The enemy couldn't be found: ");
        }

    }

    IEnumerator move(float offsetMoveAnimation, float moveVelocity)
    {
        yield return new WaitForSeconds(offsetMoveAnimation);
        // frogAnimator.SetBool("isMoving", true);
        EnemyAnimator.SetBool("isMoving", true);
        EnemyRigidBody2D.velocity = new Vector2(moveVelocity, 0f);
    }

   
   
    public IEnumerator Bounce()
    {
        // Changing the animation from  moving to Idle
        EnemyRigidBody2D.velocity = new Vector2(0, 0);
        EnemyAnimator.SetBool("isMoving", false);

        // Attack Animation/ Separate stun action for Barrel
        if(currentEnemy.Tag == "Barrel")
        {
            EnemyAnimator.SetBool("isStunned", true);
            yield return new WaitForSeconds(currentEnemy.OffsetAttackAnimation);
            EnemyAnimator.SetBool("isStunned", false);
        }
        else 
        {
            yield return new WaitForSeconds(currentEnemy.OffsetAttackAnimation);
            EnemyAnimator.SetTrigger("Attack");

            // Additional Cannon attack for cannon enemys
            if (currentEnemy.Tag == "Cannon")
            {
                yield return new WaitForSeconds(currentEnemy.OffsetAttackAnimation);
                StartCoroutine(CannonBallAtack());
            }

        }

        // Reverse frog and position of the frog
        yield return new WaitForSeconds(currentEnemy.OffsetChangePosition);
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

        yield return new WaitForSeconds(currentEnemy.OffsetVelocityDifferentWay);
        EnemyAnimator.SetBool("isMoving", true);
        EnemyRigidBody2D.velocity = new Vector2(currentEnemy.MoveVelocity *= -1f, 0);
    }


    private IEnumerator CannonBallAtack()
    {
       Transform EnemySpawner = gameObject.transform.Find("CannonSpawner");

       GameObject bullet = Instantiate(cannonBall, new Vector3(EnemySpawner.position.x,  EnemySpawner.position.y), Quaternion.identity );

        Rigidbody2D cannonRigidBody2d = bullet.GetComponent<Rigidbody2D>();

        if(isFacingRight)
        {
            cannonRigidBody2d.velocity = new Vector2(-10f, 0);
        } else if (!isFacingRight)
        {
            cannonRigidBody2d.velocity = new Vector2(10f, 0);
        }

        yield return new WaitForSeconds(1f);
    }

}
