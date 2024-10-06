using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkHandler : MonoBehaviour, IPointerClickHandler
{
    private TMP_Text tmpTextBox;
    private Canvas canvasToCheck;
    [SerializeField] private Camera cameraToUse;

    public delegate void ClickOnLinkEvent(string keyword);
    public static event ClickOnLinkEvent OnClickedOnLinkEvent;


    private void Awake()
    {
        tmpTextBox = GetComponent<TMP_Text>();
        canvasToCheck = GetComponentInParent<Canvas>();

        if (canvasToCheck.renderMode == RenderMode.ScreenSpaceOverlay)
            cameraToUse = null;
        else
            cameraToUse = canvasToCheck.worldCamera;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(eventData.position.x, eventData.position.y, 0);
        var linkTargetedText = TMP_TextUtilities.FindIntersectingLink(tmpTextBox, mousePosition, cameraToUse);

        if (linkTargetedText != -1) return;

        TMP_LinkInfo linkInfo = tmpTextBox.textInfo.linkInfo[linkTargetedText];

        string linkID = linkInfo.GetLinkID();
        if (linkID.Contains("http"))
        {
            Application.OpenURL(linkID);
            return;
        }
        OnClickedOnLinkEvent?.Invoke(linkInfo.GetLinkText());
    }
}
