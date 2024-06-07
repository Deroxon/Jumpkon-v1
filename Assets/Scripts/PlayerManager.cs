using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject player;
    public double CountFalled;
    [SerializeField] public Rigidbody2D rigidbody2D;
    [SerializeField] public CapsuleCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // need to check if the currentGameObject is not null, floating in air
        if(PlayerMovement.Instance.currentGameObject == null)
        {
            // saving the velocity number, i need to think if we should use the OnCollisionExit instead of this
            CountFalled = rigidbody2D.velocity.y;

        } 

        if (CountFalled < -30)
        {
            FallDamage();
        }
      
    }


   private void FallDamage()
    {
        if(PlayerMovement.Instance.currentGameObject != null)
        {
            StartCoroutine(GameManager.Instance.LoseHealth(1));
            CountFalled = 0;
        }
           
    }

}
