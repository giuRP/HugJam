using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region SINGLETON

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!");
            return null;
        }

        GameObject objectToSpawn = DequeueFromPool(tag);

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    /// <summary>
    /// Spawn pool with duration
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, float duration)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!");
            return null;
        }

        GameObject objectToSpawn = DequeueFromPool(tag);

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn(duration);
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject DequeueFromPool(string tag) 
    {
        foreach (Pool pool in pools) 
        {
            if (pool.tag != tag)
                continue;

            if (poolDictionary[tag].Count == 0) 
            {
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.transform.SetParent(gameObject.transform);
                    obj.SetActive(false);
                    poolDictionary[tag].Enqueue(obj);
                }
            }

            return poolDictionary[tag].Dequeue();
        }

        return null;
    }

    public void SpawnFromPoolAndAddParent(string tag, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject objectToSpawn = SpawnFromPool(tag, position, rotation);
        if(objectToSpawn != null)
            objectToSpawn.transform.SetParent(parent);

        objectToSpawn.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Custom duration
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    /// <param name="duration"></param>
    public void SpawnFromPoolAndAddParent(string tag, Vector3 position, Quaternion rotation, Transform parent, float duration)
    {
        GameObject objectToSpawn = SpawnFromPool(tag, position, rotation, duration);
        if (objectToSpawn != null)
            objectToSpawn.transform.SetParent(parent);

        objectToSpawn.transform.localPosition = position;
    }
}
