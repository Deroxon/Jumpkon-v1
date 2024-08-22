using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "test1")
            {
                StartCoroutine(test1());
            }
            else if (SceneManager.GetActiveScene().name == "test2")
            {
                StartCoroutine(test2());
            }

        }
    }
    private IEnumerator test1() 
    {
        StartCoroutine(GameManager.Instance.backToCheckPoint());
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("test2");
    }
    private IEnumerator test2()
    {
        StartCoroutine(GameManager.Instance.backToCheckPoint());
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("test1");
    }
}
