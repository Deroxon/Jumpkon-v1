using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileScript : MonoBehaviour
{
    Rigidbody2D enemyMissileRigidBody2d;
    private float MissileDirection;
    private float timer = 0f;
    private float interval = 0.2f;
    private Animator missileAnimator;

    private void Start()
    {
        missileAnimator = gameObject.GetComponent<Animator>();
        enemyMissileRigidBody2d = gameObject.GetComponent<Rigidbody2D>();
        if (enemyMissileRigidBody2d.velocity.x > 0)
        {
            MissileDirection = 2f;
        } else
        {
            MissileDirection = -2f;
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= interval)
        {
            float newSpeed = enemyMissileRigidBody2d.velocity.x;
            newSpeed += MissileDirection;
            enemyMissileRigidBody2d.velocity = new Vector2(newSpeed, 0);
            timer = 0f;
        }
        // safety check to make sure the cannonBall will be deleted
        if(enemyMissileRigidBody2d.velocity.x > 200)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyMissileRigidBody2d.velocity = new Vector2(0, 0);
            missileAnimator.SetTrigger("destroy");
            StartCoroutine(GameManager.Instance.LoseHealth(1));
            StartCoroutine(destroyMissile());
        }
        else if (collision.name == "Ground" || collision.name == "Obstacles" )
        {
            enemyMissileRigidBody2d.velocity = new Vector2(0, 0);
            missileAnimator.SetTrigger("destroy");
            StartCoroutine(destroyMissile());
        }
    }

    private IEnumerator destroyMissile()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

}
