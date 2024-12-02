using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    private GameObject thanksForPlayingBackground;
    public Animator screenAnimator;
    public GameObject ThanksForPlaying;
    public GameObject Credits;
    public GameObject BlackScreen;

    private void Update()
    {
        thanksForPlayingBackground = GameObject.Find("ThanksForPlaying");
        if (thanksForPlayingBackground != null)
        {
            if (thanksForPlayingBackground.activeInHierarchy)
            {
                if(Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.Space) )
                {
                    SceneManager.LoadScene("Credits");
                }
            }
        }
        
    }

 

    // Funkcja do wywo³ania efektu fade in

    private void Start()
    {
        thanksForPlayingBackground = GameObject.Find("Credits");
        if (DontDestroy.Instance != null) DontDestroy.Instance.menu();
        StartCoroutine(FadeToClear());
       
    }

    public IEnumerator FadeToClear()
    {
        yield return new WaitForSeconds(0.5f);
        screenAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(thanksForPlayingBackground == null ? 15f : 3f);
        StartCoroutine(FadeToBlack());
    }

    // Funkcja do wywo³ania efektu fade out
    [ContextMenu("fadeOut")]
    public IEnumerator FadeToBlack()
    {
        yield return new WaitForSeconds(0.2f);
        screenAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        if(ThanksForPlaying != null)
        {
             ThanksForPlaying.SetActive(false);
            thanksForPlayingBackground = null;
        }
       

        Debug.Log("fadeBlack ended");
         if (Credits == null)
         {
            AudioManager.Instance.StopMusic();
            SceneManager.LoadScene("Credits");
            
          } else
            {
            SceneManager.LoadScene("MainMenu");
            }
    }



}
