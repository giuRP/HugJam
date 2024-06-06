using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public void SetText(string contentText, string headerText = "")
    {
        headerField.text = headerText;
        contentField.text = contentText;

        int headerLenght = headerField.text.Length;
        int contentLenght = contentField.text.Length;

        layoutElement.enabled = (headerLenght > characterWrapLimit || contentLenght > characterWrapLimit) ? true : false;

    }

    //Optional
    private IEnumerator TypeText(string text)
    {
        yield return new WaitForSeconds(1);
    }

}
