using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float speed = 5f;
    private Vector3 UIworldPoint;

    [SerializeField]
    private float delayToStartMoving = 0.5f;
    private bool canMove;

    public void OnObjectSpawn()
    {
        UIworldPoint = FindObjectOfType<MoneyUI>().GetUIworldPosition();
        canMove = false;

        Invoke("StartMoving", delayToStartMoving);
    }

    public void OnObjectSpawn(float customUnspawnTime)
    {
        throw new System.NotImplementedException();
    }

    private void StartMoving()
    {
        canMove = true;
    }

    private void Update()
    {
        if (!canMove)
            return;

        float distance = Vector3.Distance(UIworldPoint, transform.position);

        if (distance > 0.01f)
        {
            float step = speed * Time.deltaTime;
            Vector3 targetPosition = Vector3.MoveTowards(transform.position, UIworldPoint, step);
            transform.position = targetPosition;
        }
        else
        {
            MoneyManager.instance.GainMoney(1);
            gameObject.SetActive(false);
        }
    }
}
