using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectFromPool : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float timeToUnspawn = 0.5f;

    public void OnObjectSpawn()
    {
        StartCoroutine(Unspawn());
    }

    private IEnumerator Unspawn() 
    {
        yield return new WaitForSeconds(timeToUnspawn);

        gameObject.SetActive(false);
    }

    public void OnObjectSpawn(float customUnspawnTime)
    {
        StartCoroutine(Unspawn(customUnspawnTime));
    }

    private IEnumerator Unspawn(float unspawnTime)
    {
        yield return new WaitForSeconds(unspawnTime);

        gameObject.SetActive(false);
    }
}
