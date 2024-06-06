using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTileAnimations : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private SpriteRenderer tileGFX;
    [SerializeField]
    private SpriteRenderer plantGFX;
    [SerializeField]
    private GameObject growParticles;
    [SerializeField]
    private GameObject completedSparkles;

    [Space(5)]
    [Header("Sprites")]
    [SerializeField]
    private Sprite wetTileGFX;
    [SerializeField]
    private Sprite dryTileGFX;
    [SerializeField]
    private Sprite growingSprite;
    [SerializeField]
    private Sprite completedSprite;

    private PlantStateEnum currentPlantState = PlantStateEnum.EMPTY;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerHoverAnimation() 
    {
        if (currentPlantState == PlantStateEnum.EMPTY)
            return;

        if (!anim)
            anim = GetComponent<Animator>();

        anim.SetTrigger("Hover");
    }

    public void TriggerSpawnPlantAnimation()
    {
        if (!anim)
            anim = GetComponent<Animator>();

        plantGFX.gameObject.SetActive(true);
        anim.SetTrigger("Spawn");
    }

    public void SetAnimationState(PlantStateEnum plantState) 
    {
        if (!anim)
            anim = GetComponent<Animator>();

        currentPlantState = plantState;

        anim.SetBool("EmptyState", currentPlantState == PlantStateEnum.EMPTY);
        plantGFX.gameObject.SetActive(currentPlantState != PlantStateEnum.EMPTY);
        anim.SetBool("GrowingState", currentPlantState == PlantStateEnum.GROWING);
        growParticles.SetActive(currentPlantState == PlantStateEnum.GROWING);
        anim.SetBool("CompletedState", currentPlantState == PlantStateEnum.COMPLETED);
        completedSparkles.SetActive(currentPlantState == PlantStateEnum.COMPLETED);

        plantGFX.sprite = (currentPlantState != PlantStateEnum.COMPLETED) ? growingSprite : completedSprite;
    }

    public void OnTileStateChanged(GroundState newTileState) 
    {
        switch (newTileState) 
        {
            default:
            case GroundState.WET:
                tileGFX.sprite = wetTileGFX;
                break;
            case GroundState.DRY:
                tileGFX.sprite = dryTileGFX;
                break;
        }
    }
}
