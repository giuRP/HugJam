using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager instance;

    private Volume globalVolume;
    private Vignette vignette;
    private ColorAdjustments colorAdjustment;

    #region INITIAL VALUES
    //Vignette
    private Color vigColor;
    private float vigIntensity;
    //Color Adjustment
    private Color colorAdjColor;
    #endregion

    #region SETTING INITIAL VALUES
    private void Awake()
    {
        instance = this;

        globalVolume = GetComponent<Volume>();
        globalVolume.profile.TryGet(out vignette);
        globalVolume.profile.TryGet(out colorAdjustment);
    }

    private void Start()
    {
        vigColor = vignette.color.value;
        vigIntensity = vignette.intensity.value;

        colorAdjColor = colorAdjustment.colorFilter.value;
    }

    private void OnEnable()
    {
        StartCoroutine(SubscribeDelegate());
    }

    private IEnumerator SubscribeDelegate() 
    {
        yield return new WaitForSeconds(0.1f);

        CO2Manager.Instance.OnCO2Change += OnCO2Changed;
    }

    private void OnDisable()
    {
        CO2Manager.Instance.OnCO2Change -= OnCO2Changed;
    }

    private void OnCO2Changed()
    {
        if (CO2Manager.Instance.currentCo2 > 0.6 * CO2Manager.Instance.maxCo2)
        {
            SetVignetteIntensity(0.5f * (CO2Manager.Instance.currentCo2 / CO2Manager.Instance.maxCo2));
        }
        else 
        {
            SetVignetteIntensity(0f);
        }
    }
    #endregion

    public void SetVignetteIntensity(float intensity) 
    {
        vignette.intensity.value = intensity;
        vigIntensity = intensity;
    }

    public void SetTemporaryVignette(Color color, float intensity, float time) 
    {
        StartCoroutine(SetTempVignette(color, intensity, time));
    }

    private IEnumerator SetTempVignette(Color color, float intensity, float time) 
    {
        vignette.color.value = color;
        vignette.intensity.value = intensity;

        yield return new WaitForSeconds(time);

        vignette.color.value = vigColor;
        vignette.intensity.value = vigIntensity;
    }

    public void SetColorAdjustmentColor(Color color) 
    {
        colorAdjustment.colorFilter.value = color;
    }

}
