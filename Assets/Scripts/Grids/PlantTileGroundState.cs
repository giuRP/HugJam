using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTileGroundState : MonoBehaviour
{
    public delegate void GroundStateChanged(GroundState newGroundState);
    public GroundStateChanged OnGroundStateChanged;

    private GroundState groundState;

    private void Start()
    {
        float rand = Random.Range(0f, 100f);

        if (rand < 30)
            SetGroundState(GroundState.DRY);

        SwitchOnGroundState(groundState);
    }

    public void SetGroundState(GroundState newGroundState) 
    {
        groundState = newGroundState;
        OnGroundStateChanged?.Invoke(newGroundState);
        SwitchOnGroundState(newGroundState);
    }

    public GroundState GetGroundState() 
    {
        return groundState;
    }

    private void SwitchOnGroundState(GroundState groundState) 
    {
        switch (groundState) 
        {
            case GroundState.WET:
                StopAllCoroutines();
                float rand = Random.Range(10f, 80f);
                StartCoroutine(StartWetTimer(rand));
                break;
            case GroundState.DRY:
                break;

        }
    }

    public void UpdateWetTimer()
    {
        /*StopAllCoroutines();
        float rand = Random.Range(ClimateManager.Instance.minTime, ClimateManager.Instance.maxTime);
        StartCoroutine(StartWetTimer(rand));*/
    }

    private IEnumerator StartWetTimer(float time) 
    {
        yield return new WaitForSeconds(time);

        SetGroundState(GroundState.DRY);
    }
}
