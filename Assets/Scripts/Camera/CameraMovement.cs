using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField]
    private float speed = 0;

    private float maxSpeed = 10, accel = 1;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnEnable()
    {
        UpgradeGridEventUtil.OnUpgradeGrid += MoveToUpgradedGrid;
    }

    private void OnDisable()
    {
        UpgradeGridEventUtil.OnUpgradeGrid -= MoveToUpgradedGrid;
    }

    public void CalculateVelocity(Vector2 movementDirection)
    {
        rb2D.velocity = movementDirection * speed;
        //rb2D.velocity = movementDirection * CalculateSpeed();
    }

    private float CalculateSpeed()
    {
        speed += accel * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0, maxSpeed);

        return speed;
    }

    private void MoveToUpgradedGrid()
    {
        StartCoroutine(MoveToUpgradedGridDelay());
    }

    IEnumerator MoveToUpgradedGridDelay()
    {
        yield return new WaitForEndOfFrame();
        rb2D.AddForce(new Vector2(1, 0) * 50, ForceMode2D.Impulse);
    }
}
