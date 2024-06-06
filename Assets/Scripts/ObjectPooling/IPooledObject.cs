using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    public void OnObjectSpawn();
    
    /// <summary>
    /// With custom duration 
    /// </summary>
    /// <param name="customUnspawnTime"></param>
    public void OnObjectSpawn(float customUnspawnTime);
}
