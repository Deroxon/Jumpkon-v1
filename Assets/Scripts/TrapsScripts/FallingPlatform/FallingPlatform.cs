using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingPlatform : MonoBehaviour
{

    private bool triggeredStand;
    public Animator FallingPlatformAnimator;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            FallingPlatformAnimator.SetTrigger("isStanding");
            AudioManager.Instance.PlaySFX("PlatformBreak");
            StartCoroutine(deletePlatform());
        }
    }


    public IEnumerator deletePlatform()
    {

        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }


}
