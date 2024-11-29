using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheckScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement.Instance.currentGameObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement.Instance.currentGameObject = null;
    }

}
