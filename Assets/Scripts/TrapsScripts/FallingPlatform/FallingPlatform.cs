using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingPlatform : MonoBehaviour
{

    private bool triggeredStand;
    public Animator FallingPlatformAnimator;
    private bool isStanding = true;
    private Vector3 GameObjectPosition;
    public GameObject repawnPlatform;

    private void Start()
    {
        GameObjectPosition = new Vector3Double(gameObject.transform.position.x, gameObject.transform.position.y, 0).ToVector3();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && isStanding)
        {
            FallingPlatformAnimator.SetTrigger("isStanding");
            AudioManager.Instance.PlaySFX("PlatformBreak");
            StartCoroutine(deletePlatform());
            isStanding = false;
        }
    }


    public IEnumerator deletePlatform()
    {
        if (gameObject.CompareTag("RespawnPlatform") )
        {
            yield return new WaitForSeconds(1.2f);
            GameManager.Instance.InitializeSpawnPlatform(GameObjectPosition, gameObject);
        } else
        {
            yield return new WaitForSeconds(1.2f);
            Destroy(this.gameObject);
        }
    }


}
