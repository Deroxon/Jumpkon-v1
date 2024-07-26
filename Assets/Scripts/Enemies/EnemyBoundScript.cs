using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoundScript : MonoBehaviour
{
    private EnemyLogicScript enemyLogicScript;
    public GameObject Enemy;
    private Transform EnemyContainer;
    void Start()
    {
        enemyLogicScript = Enemy.GetComponent<EnemyLogicScript>();
        EnemyContainer = transform.parent;
        Debug.Log(EnemyContainer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // need to think if we need to change it to finding by "Tag" in the Objects list
        if (GameManager.Instance.EnemiesTags.Contains(collision.tag) &&  collision.transform.IsChildOf(EnemyContainer) )
        {
            Debug.Log("triggered");
            StartCoroutine(enemyLogicScript.Bounce());
        }
    }
}
