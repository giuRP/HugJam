using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimateManager : Singleton<ClimateManager>
{
    public event Action OnClimateChangeToDry;
    public event Action OnClimateChangeToWet;

    public float minPlantTime = 1, maxPlantTime = 1;

    [SerializeField]
    private float timeToClimateChange = 1;
    [SerializeField]
    private float wetMinTime, wetMaxTime;
    [SerializeField]
    private float dryMinTime, dryMaxTime;

    [SerializeField]
    private bool isClimateDry = false;

    private Timer timer;

    private void Start()
    {
        ChangeClimate();
        StartCoroutine(ChangeClimateCoroutine());
    }

    private void ChangeClimate()
    {
        if (isClimateDry)
        {
            //Troca o clima para DRY
            OnClimateChangeToDry?.Invoke();
            minPlantTime = dryMinTime;
            maxPlantTime = dryMaxTime;
        }
        else
        {
            //Troca o clima para WET
            OnClimateChangeToWet?.Invoke();
            minPlantTime = wetMinTime;
            maxPlantTime = wetMaxTime;
        }
    }

    IEnumerator ChangeClimateCoroutine()
    {
        yield return new WaitForSeconds(timeToClimateChange);

        isClimateDry = !isClimateDry;

        ChangeClimate();

        StartCoroutine(ChangeClimateCoroutine());
    }
}
