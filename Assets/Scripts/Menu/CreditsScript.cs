using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
 
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            AudioManager.Instance.StopMusic();
            SceneManager.LoadScene("MainMenu");
        }
    }

    public Animator screenAnimator;  
    public GameObject ThanksForPlaying;
    public GameObject Credits;
    public GameObject BlackScreen;
    public int creditsAgain = 0;

    // Funkcja do wywo³ania efektu fade in

    private void Start()
    {
        if (DontDestroy.Instance != null) DontDestroy.Instance.menu();
        StartCoroutine(FadeToClear());
       
    }

    public IEnumerator FadeToClear()
    {
        Debug.Log("FadeToClear");
        yield return new WaitForSeconds(1f);
        screenAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(creditsAgain == 1 ? 20f : 9f);
        StartCoroutine(FadeToBlack());
    }

    // Funkcja do wywo³ania efektu fade out
    [ContextMenu("fadeOut")]
    public IEnumerator FadeToBlack()
    {
        Debug.Log("FadeToBlack");
        yield return new WaitForSeconds(0.2f);
        screenAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        ThanksForPlaying.SetActive(false);
        creditsAgain++;
        if (creditsAgain == 1)
        {
            Credits.SetActive(true);
            StartCoroutine(FadeToClear());
        } else if (creditsAgain == 2)
        {
            AudioManager.Instance.StopMusic();
            SceneManager.LoadScene("MainMenu");
        }
    }



}
