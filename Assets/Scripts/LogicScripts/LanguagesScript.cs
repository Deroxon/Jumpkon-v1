using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


public class LanguagesScript : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;

    // Start is called before the first frame update
    void Start()
    {
        // £adowanie aktualnie wybranego jêzyka
        string currentLanguage = PlayerPrefs.GetString("selectedLanguage", "en");
        SetLanguage(currentLanguage);

        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
    }

    // Ustawienie jêzyka
    public void OnLanguageChanged(int index)
    {
        string selectedLanguage = GetLanguageFromDropdown(index);
        PlayerPrefs.SetString("selectedLanguage", selectedLanguage);
        PlayerPrefs.Save();

        SetLanguage(selectedLanguage);
    }

    // Metoda do ustawienia jêzyka
    private void SetLanguage(string language)
    {
        Locale locale = LocalizationSettings.AvailableLocales.Locales.Find(l => l.Identifier.Code == language);
        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
        }
    }

    // Mapowanie indeksu Dropdown na kod jêzyka
    private string GetLanguageFromDropdown(int index)
    {
        switch (index)
        {
            case 0: return "en"; // English
            case 1: return "zh-Hans"; // Chinese Simplified
            case 2: return "fr"; // French
            case 3: return "de"; // German
            case 4: return "it"; // Italian
            case 5: return "pl"; // Polish
            case 6: return "ru"; // Russian
            case 7: return "uk"; // Ukrainian
            default: return "en";
        }
    }

}
