using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlantStateEnum
{ 
    EMPTY,
    SEED,
    GROWING,
    COMPLETED
}

public class PlantTileStateMachine : MonoBehaviour
{
    public UnityEvent OnStartPlant;
    public UnityEvent OnFinishPlant;

    private PlantState currentState;
    private PlantStateEnum currentStateEnum;

    private Dictionary<PlantStateEnum, PlantState> plantStateDict;
   

    public int extraBerries = 0;
    
    public float growthModifier;

    private void Start()
    {
        BuildStates();
        ChangeState(PlantStateEnum.EMPTY);
    }

    private void BuildStates()
    {
        plantStateDict = new Dictionary<PlantStateEnum, PlantState>();

        plantStateDict.Add(PlantStateEnum.EMPTY, new EmptyState());;
        plantStateDict.Add(PlantStateEnum.GROWING, new GrowingState());
        plantStateDict.Add(PlantStateEnum.COMPLETED, new CompletedState());
    }

    public void ChangeState(PlantStateEnum newState)
    {
        currentState = plantStateDict[newState];
        currentStateEnum = newState;
        currentState.Enter(this);
    }

    public void AddGrowthModifier(float modifierValue)
    {
        growthModifier = modifierValue; 
    }

    public void AddExtraBerries(int _berries)
    {
        extraBerries = _berries;
    }

    public void OnClicked() 
    {
        PlantTileGroundState plantGroundState = GetComponent<PlantTileGroundState>();
        if (plantGroundState.GetGroundState() == GroundState.DRY && currentStateEnum != PlantStateEnum.COMPLETED)
            return;

        currentState.OnClicked();
    }
}

public abstract class PlantState
{
    protected Timer timer;

    public abstract void Enter(PlantTileStateMachine _stateMachine);

    public abstract void OnClicked();
}

public class EmptyState : PlantState
{
    private PlantTileStateMachine stateMachine;

    public override void Enter(PlantTileStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;

        PlantTileAnimations plantAnim = stateMachine.GetComponent<PlantTileAnimations>();
        plantAnim.SetAnimationState(PlantStateEnum.EMPTY);
    }

    public override void OnClicked()
    {
        timer = stateMachine.GetComponent<Timer>();

        timer.StartTimer(5f + stateMachine.growthModifier, StartGrowing);

        stateMachine.OnStartPlant?.Invoke();
    }

    private void StartGrowing() 
    {
        stateMachine.ChangeState(PlantStateEnum.GROWING);
    }
}

public class GrowingState : PlantState
{
    private PlantTileStateMachine stateMachine;

    public override void Enter(PlantTileStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;

        PlantTileAnimations plantAnim = stateMachine.GetComponent<PlantTileAnimations>();
        plantAnim.TriggerSpawnPlantAnimation();
        plantAnim.SetAnimationState(PlantStateEnum.SEED);
    }

    public override void OnClicked()
    {
        Timer timer = stateMachine.GetComponent<Timer>();

        timer.StartTimer(7f + stateMachine.growthModifier, CompleteGrowth);

        stateMachine.OnStartPlant?.Invoke();

        PlantTileAnimations plantAnim = stateMachine.GetComponent<PlantTileAnimations>();
        plantAnim.SetAnimationState(PlantStateEnum.GROWING);
    }

    private void CompleteGrowth() 
    {
        stateMachine.ChangeState(PlantStateEnum.COMPLETED);
    }
}

public class CompletedState : PlantState
{
    private PlantTileStateMachine stateMachine;

    public override void Enter(PlantTileStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;

        PlantTileAnimations plantAnim = stateMachine.GetComponent<PlantTileAnimations>();
        plantAnim.SetAnimationState(PlantStateEnum.COMPLETED);
    }

    public override void OnClicked()
    {
        stateMachine.ChangeState(PlantStateEnum.EMPTY);

        stateMachine.OnFinishPlant?.Invoke();

        int totalBerries = 2 + stateMachine.extraBerries;

        for (int i = 0; i < totalBerries; i++)
        {
            Vector2 offSet = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
            ObjectPooler.Instance.SpawnFromPool("Berry", (Vector2)stateMachine.transform.position + offSet, Quaternion.identity);
        }
    }
}
