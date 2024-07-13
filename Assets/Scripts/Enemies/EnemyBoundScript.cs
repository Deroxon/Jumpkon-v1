using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBoundScript : MonoBehaviour
{
    private EnemyLogicScript enemyLogicScript;
    public GameObject Enemy;
    private Transform EnemyContainer;
    void Start()
    {
        enemyLogicScript = Enemy.GetComponent<EnemyLogicScript>();
        EnemyContainer = transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // need to think if we need to change it to finding by "Tag" in the Objects list
        if (GameManager.Instance.EnemiesTags.Contains(collision.tag) &&  collision.transform.IsChildOf(EnemyContainer))
        {
           StartCoroutine(enemyLogicScript.Bounce());
        }
    }
}
