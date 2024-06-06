using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundState
{
    WET,
    DRY
}

public class PlantTile : MonoBehaviour, IClickable
{
    [Header("References")]
    [SerializeField]
    private PlantTileStateMachine stateMachine;
    [SerializeField]
    private PlantTileAnimations plantAnim;
    [SerializeField]
    private PlantTileGroundState plantGroundState;

    private void OnEnable()
    {
        plantGroundState.OnGroundStateChanged += OnGroundStateChanged;
    }

    private void OnDisable()
    {
        plantGroundState.OnGroundStateChanged -= OnGroundStateChanged;
    }

    public void OnClicked()
    {
        stateMachine.OnClicked();
    }

    public void OnHover()
    {
        plantAnim.TriggerHoverAnimation();
    }

    public void OnUnhover()
    {

    }

    private void OnGroundStateChanged(GroundState _tileState) 
    {
        plantAnim.OnTileStateChanged(_tileState);
    }
}
