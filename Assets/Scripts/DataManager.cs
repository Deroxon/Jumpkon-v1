using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public int[] modes = new int[3] { 1, 2, 3 };
    public int currentMode;
    public Resolution[] resolutions;
    public int resolutionsCount;
    public int currentResolution;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
