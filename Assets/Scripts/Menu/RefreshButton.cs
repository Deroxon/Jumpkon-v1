using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshButton : MonoBehaviour
{

    private Button thisButton;
    private DataBaseManager dbGM;
    // Start is called before the first frame update
    void Start()
    {
        thisButton = gameObject.GetComponent<Button>();

        dbGM = GameObject.Find("DataBaseManager").GetComponent<DataBaseManager>();

        thisButton.onClick.AddListener(refresh);
    }

    public void refresh()
    {
        dbGM.CheckPossibilitytoFetch();
    }

}
