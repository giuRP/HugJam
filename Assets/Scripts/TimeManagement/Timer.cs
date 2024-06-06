using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Image timerFill;
    [SerializeField]
    private GameObject timerUI;

    private float timeToTrigger;
    private float timeCount;
    private Action callback;
    protected float speedMult = 1f;
    private bool activated;

    public void StartTimer(float _time, Action _callback) 
    {
        if (activated)
            return;

        timeCount = 0;
        timeToTrigger = _time;
        callback = _callback;
        SetActivated(true);
    }

    private void Update()
    {
        if (!activated)
            return;

        CountTime();
        UpdateUI();
    }

    private void CountTime() 
    {
        timeCount += Time.deltaTime * speedMult;

        if (timeCount >= timeToTrigger)
        {
            timeCount = 0;
            callback?.Invoke();
            SetActivated(false);
        }
    }

    private void SetActivated(bool result) 
    {
        activated = result;

        if (ContainsUI())
            timerUI.SetActive(activated);

    }

    private void UpdateUI() 
    {
        if (!ContainsUI())
            return;

        timerFill.fillAmount = timeCount / timeToTrigger;
    }

    private bool ContainsUI() 
    {
        return (timerFill != null && timerUI != null);
    }
}
