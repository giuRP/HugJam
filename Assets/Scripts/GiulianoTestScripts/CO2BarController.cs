using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CO2BarController : MonoBehaviour
{
    [SerializeField]
    private Image barFill;
    [SerializeField]
    private Color positiveColor;
    [SerializeField]
    private Color negativeColor;

    [SerializeField]
    private List<float> limiter_YPositions;

    [SerializeField]
    private RectTransform limiterBar;

    private UIShakeElement shakeEffect;

    private void Awake()
    {
        barFill = GetComponent<Image>();
        shakeEffect = GetComponent<UIShakeElement>();
    }

    private void Start()
    {
        if (CO2Manager.Instance.co2BarController == null)
        {
            CO2Manager.Instance.co2BarController = this;
        }

        barFill.fillAmount *= .5f;
        limiterBar.localPosition = new Vector3(limiterBar.localPosition.x, limiter_YPositions[0], limiterBar.localPosition.z);
    }

    public void UpdateFillAmont(float value)
    {
        barFill.fillAmount = (value / CO2Manager.Instance.maxCo2);

        barFill.color = Color.Lerp(positiveColor, negativeColor, value / (CO2Manager.Instance.maxCo2 * 0.75f));
    }

    public void PlayBarVisualEffects()
    {
        shakeEffect.PlayShakeRotation();
    }

    public void SetLimiterPosition(float co2MinValue)
    {
        int index = 0;

        switch (co2MinValue)
        {
            case 300:
                index = 0;
                break;
            case 200:
                index = 1;
                break;
            case 100:
                index = 2;
                break;
            case 0:
                index = 3;
                break;
            default:
                break;
        }

        limiterBar.localPosition = new Vector3(limiterBar.localPosition.x, limiter_YPositions[index], limiterBar.localPosition.z);
    }
}
