using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakPoint : MonoBehaviour
{
    private Transform EnemyContainer;
    private Transform Enemy;

    // The implementation works for this structure, the hitbox is nested in "enemy" by itself which has the Animator and grabing the EnemyContainer is Resposible for the grabing all thing and Destroying it 

    private void Start()
    {
        // grabing parents of nested gameobject
        EnemyContainer = transform.parent.parent;
        Enemy = transform.parent;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // jumping and startingCoroutine of destroying the enemy
        if(collision.gameObject.CompareTag("Player") && !GameManager.Instance.isImmortal)
        {

            PlayerManager.Instance.playerRigidbody2D.velocity = new Vector2(PlayerManager.Instance.playerRigidbody2D.velocity.x, 14);
            StartCoroutine(DestroyEnemy());
        }
    }

   private IEnumerator DestroyEnemy()
    {
        destroyEnemyAnimation();
        yield return new WaitForSeconds(0.5f);
        Destroy(EnemyContainer.gameObject);
    }

    public void destroyEnemyAnimation()
    {
        Animator animator = Enemy.GetComponent<Animator>();
        animator.SetTrigger("isHitted");
    }


}
