using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class clickButton : MonoBehaviour
{
    private Button thisButton;
    private void Start()
    {
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(PlayClick);
    }

    private void PlayClick()
    {
        AudioManager.Instance.PlaySFX("Click");
    }
}
