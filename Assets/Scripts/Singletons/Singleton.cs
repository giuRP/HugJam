using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    SetupInstance();
                }
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        RemoveInstanceDuplicates();
    }

    private void RemoveInstanceDuplicates()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Debug.Log(gameObject.name);
            Destroy(gameObject);
        }
    }

    private static void SetupInstance()
    {
        instance = (T)FindObjectOfType(typeof(T));

        if (instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;
            instance = gameObj.AddComponent<T>();
        }
    }
}
