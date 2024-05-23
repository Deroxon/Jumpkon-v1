using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private LogicScript logic;
    [SerializeField] private Animator endAnimation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        logic.victory();
        endAnimation.SetTrigger("Interact");
    }
}
