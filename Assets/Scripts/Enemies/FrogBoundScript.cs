using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBoundScript : MonoBehaviour
{
    private EnemyLogicScript frogscript;
    public GameObject frog;
    private Transform EnemyContainer;
    void Start()
    {
        frogscript = frog.GetComponent<EnemyLogicScript>();
        EnemyContainer = transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.EnemiesTags.Contains(collision.tag) &&  collision.transform.IsChildOf(EnemyContainer))
        {
           StartCoroutine(frogscript.Bounce());
        }
    }
}
