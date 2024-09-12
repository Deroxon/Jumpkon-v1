using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushScript : Singleton<CrushScript>
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (layerName != "BoundLayer" && layerName != "PlatformsLayer" && layerName != "CheckPointLayer" &&  GameManager.Instance.EnemiesTags.Contains(collision.tag))
        {
            StartCoroutine(PlayerMovement.Instance.AnimationDamage());
            StartCoroutine(GameManager.Instance.LoseHealth(1));
        }
    }
}
