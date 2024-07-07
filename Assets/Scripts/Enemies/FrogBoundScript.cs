using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBoundScript : MonoBehaviour
{
    private FrogScript frogscript;
    public GameObject frog;
     void Start()
    {
        frogscript = frog.GetComponent<FrogScript>();
    }

    private void OnTriggerEnter2D(Collider2D platform)
    {
        if (platform.CompareTag("Frog"))
        {
           StartCoroutine(frogscript.Bounce());
        }
    }
}
