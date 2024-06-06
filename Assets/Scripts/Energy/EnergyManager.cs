using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : Singleton<EnergyManager>
{
    public int currentEnergyPoints;

    public int maxEnergy;

    public delegate void EnergyChanged(int currentEnergy);
    public EnergyChanged OnEnergyChanged;


    private void Start()
    {
        maxEnergy = 100;
        currentEnergyPoints = 50;
    }

    public void AddEnergy(int extraEnergy)
    {
        currentEnergyPoints += Mathf.Abs(extraEnergy);

        if (currentEnergyPoints > maxEnergy)
            currentEnergyPoints = maxEnergy;
        OnEnergyChanged?.Invoke(currentEnergyPoints);
    }

    public void SpendEnergy(int energySpend)
    {
        currentEnergyPoints -= energySpend;
        OnEnergyChanged?.Invoke(currentEnergyPoints);
    }

    public bool HasEnoghtEnergy(int energySpend)
    {
        return currentEnergyPoints - energySpend > 0;
    }
}
