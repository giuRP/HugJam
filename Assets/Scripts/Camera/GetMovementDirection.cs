using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMovementDirection : MonoBehaviour, IClickable
{
    private CameraMovement cameraMovement;

    [SerializeField]
    private Vector2 movementDirection = Vector2.zero;

    private void Awake()
    {
        cameraMovement = GetComponentInParent<CameraMovement>();
    }

    public void OnClicked()
    {
        //Do Nothing
    }

    public void OnHover()
    {
        //Mover a camera para a direção determida pelo colisor
        cameraMovement.CalculateVelocity(this.movementDirection);
    }

    public void OnUnhover()
    {
        //Parar de mover a camera
        cameraMovement.CalculateVelocity(Vector2.zero);
    }
}
