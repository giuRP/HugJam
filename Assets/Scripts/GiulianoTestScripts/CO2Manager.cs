using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CO2Manager : MonoBehaviour
{
    public static CO2Manager Instance;

    public float currentCo2, minCo2, maxCo2;

    public CO2BarController co2BarController;
    public PlayCO2ParticlesUtil co2Particles;

    public event Action OnCO2Change;
    public event Action OnCO2Over;

    [SerializeField]
    private float decreaseRate;

    private bool isCO2Over = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        maxCo2 = 1000f;
        minCo2 = 300f;

        currentCo2 = .5f * maxCo2;

        InvokeRepeating("WhenCO2Change", 0f, .1f);
    }

    private void Update()
    {
        if (currentCo2 <= 0 && !isCO2Over)
        {
            isCO2Over = true;
            OnCO2Over?.Invoke();
        }

        if (currentCo2 >= maxCo2)
            currentCo2 = maxCo2;

        if (currentCo2 <= minCo2)
        {
            currentCo2 = minCo2;
            return;
        }

        currentCo2 -= decreaseRate * Time.deltaTime;

        co2BarController.UpdateFillAmont(currentCo2);
    }

    public void SetDrecreaseRate(float modifier)
    {
        decreaseRate += modifier;
    }

    public void SetMinCO2Value(float value)
    {
        minCo2 = value;
        co2BarController.SetLimiterPosition(minCo2);
    }

    public void IncreaseCO2(float value)
    {
        currentCo2 += value;
        co2BarController.PlayBarVisualEffects();
        co2Particles.PlayParticles();
    }

    private void WhenCO2Change()
    {
        OnCO2Change?.Invoke();
    }
}
