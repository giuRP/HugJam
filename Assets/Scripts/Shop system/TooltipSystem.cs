using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem instance;

    public ToolTip _tooltip;

    private void Awake()
    {
        instance = this;
    }

    public void ShowTooltip(string headerText, string contentText)
    {
        _tooltip.gameObject.SetActive(true);
        _tooltip.transform.position = Input.mousePosition;
        //Play some animation
        _tooltip.SetText(contentText, headerText);
    }

    public void HideTooltip()
    {
        _tooltip.gameObject.SetActive(false);

    }
}
