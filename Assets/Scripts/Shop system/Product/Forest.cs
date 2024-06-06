using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Product
{
    [Header("Forest spawn")]
    [Range(1,4.5f)]
    public float radiusSpawnRange;
    public GameObject[] treePrefabs;

    public override void OnClicked()
    {
        //Do some animation or effect
    }

    public override bool TryUpdateLevel()
    {
        if (base.TryUpdateLevel())
        {
            Effects();
            return true;
        }
        return false;
    }

    public override void SetAffectedObjects()
    {
        Effects();
    }
    public override void Effects()
    {
        //CO2
        CO2Manager.Instance.SetMinCO2Value(currentProductStats.carbonMinValue);

        //Spawn trees
        for (int i = 0; i < 8; i++)
        {
            SpawnTree();
        }
    }

    private void SpawnTree()
    {
        Vector2 center = transform.position;

        int randomTreeIndex = Random.Range(0, treePrefabs.Length-1);
        GameObject spawnedTree = treePrefabs[randomTreeIndex];

        float randomRadius = Random.Range(1, radiusSpawnRange);

        float randomAngle = Random.Range(0, 360);

        Vector2 spawnPos;
        spawnPos.x = center.x + randomRadius * Mathf.Sin(randomAngle * Mathf.Rad2Deg);
        spawnPos.y = center.y + randomRadius * Mathf.Cos(randomAngle * Mathf.Rad2Deg);

        //Get spawn radius
        Instantiate(spawnedTree, spawnPos, Quaternion.identity);
    }

    public override void UndoEffects()
    {

    }

}
