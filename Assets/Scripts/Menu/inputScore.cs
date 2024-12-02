using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProfanityFilter;
using Unity.VisualScripting;
using System;

public class inputScore : MonoBehaviour
{
    public GameObject text;
    public GameObject description;
    public GameObject saveButton;

    private float cooldownTime = 2f;
    private DateTime lastRequestTime = DateTime.MinValue;

    private void Start()
    {
        ActivateChildren(this.gameObject);
    }

    public void saveHighScore()
    {

        TimeSpan timeSinceLastRequest = DateTime.UtcNow - lastRequestTime;

        if (timeSinceLastRequest.TotalSeconds >= cooldownTime)
        {
            string forwardedText = text.GetComponent<TextMeshProUGUI>().text.ToString();
            if (validateNickName(forwardedText))
            {
                description.GetComponent<TextMeshProUGUI>().text = "Please input name without profanity";
            }
            else
            {
                description.GetComponent<TextMeshProUGUI>().text = "Your record has been saved!";
                TimerScript.Instance.SaveTime(forwardedText);
                DeActivateChildren(this.gameObject);

                AudioManager.Instance.PlayMusic("VictoryGame");
                goToThanks();
            }
            lastRequestTime = DateTime.UtcNow;
        }
        else
        {
            // Jeœli u¿ytkownik próbuje wywo³aæ funkcjê zbyt szybko, wyœwietlamy komunikat
            description.GetComponent<TextMeshProUGUI>().text = "Too many attemps in short time. Wait 3 seconds ";
        }
        
    }

    public void goToThanks()
    {
        SceneManager.LoadScene("thanks");
    }

    public bool validateNickName(string checkText)
    {
        ProfanityFilter.ProfanityFilter filter = new ProfanityFilter.ProfanityFilter();
        return filter.ContainsProfanity(checkText);
    }



    private void ActivateChildren(GameObject parent)
    {
        // Iterujemy przez wszystkie dzieci obiektu parent
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true); // Aktywujemy ka¿de dziecko
        }
    }


    private void DeActivateChildren(GameObject parent)
    {
        // Iterujemy przez wszystkie dzieci obiektu parent
        foreach (Transform child in parent.transform)
        {
            if(child.name != "Description" && child.name != "VictoryTxt")
            {
                child.gameObject.SetActive(false); // Aktywujemy ka¿de dziecko
            }
            
        }
    }

}
