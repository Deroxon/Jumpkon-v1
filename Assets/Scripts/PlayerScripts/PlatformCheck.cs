using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCheck : Singleton<PlatformCheck>
{
    public GameObject currentPlatform;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentPlatform = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentPlatform = null;
    }
}
