using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantationGrid : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject tilePrefab;

    [Space(5)]
    [Header("Grid Properties")]
    [SerializeField]
    private int gridSize = 5;
    [SerializeField]
    private float tilesOffSet = 1f;

    private void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid() 
    {
        for (int xx = 0; xx < gridSize; xx++)
        {
            for (int yy = 0; yy < gridSize; yy++)
            {
                Vector2 posToSpawn = transform.position + new Vector3(xx * tilesOffSet, yy * tilesOffSet, 0f);

                Instantiate(tilePrefab, posToSpawn, Quaternion.identity, this.transform);
            }
        }
    }
}
