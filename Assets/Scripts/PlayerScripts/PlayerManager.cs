using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject player;
    public double CountFalled;
    private bool isGettingDamage = false;
    [SerializeField] public Rigidbody2D playerRigidbody2D;
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
            CountFalled = playerRigidbody2D.velocity.y;
            PlayerMovement.Instance.animator.SetFloat("velocityY", (float)CountFalled);
        } 

        if (CountFalled < -30 && !isGettingDamage)
        {
            isGettingDamage = !isGettingDamage;
        }
        if(CountFalled == 0)
        {
            FallDamage();
        }
      
    }


   private void FallDamage()
    {
        if( (isGettingDamage && PlayerMovement.Instance.currentGameObject.name != "Trampoline") && (isGettingDamage && PlayerMovement.Instance.currentGameObject.name != "Hay"))
        {
            StartCoroutine(GameManager.Instance.LoseHealth(1));
        }
        // jumping on the hay
        if(isGettingDamage && PlayerMovement.Instance.currentGameObject.name == "Hay")
        {
            Instance.playerRigidbody2D.velocity = new Vector2(PlayerManager.Instance.playerRigidbody2D.velocity.x, 16);
        }

        isGettingDamage = false;
    }

}
