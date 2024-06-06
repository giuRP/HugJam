using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPopUp : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isActivated = false;

    private void Awake()
    {
        isActivated = false;

        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(SubscribeOnEnable());
        //CO2Manager.Instance.OnCO2Over += PlayMoveEffectUI;
    }

    private void OnDisable()
    {
        CO2Manager.Instance.OnCO2Over -= PlayMoveEffectUI;
    }

    //private void Start()
    //{
    //    PlayMoveEffectUI();
    //}

    private void PlayMoveEffectUI()
    {
        isActivated = !isActivated;

        if (isActivated)
        {
            animator.SetTrigger("OnEnter");
        }
        else
        {
            animator.SetTrigger("OnExit");
        }
    }

    public void ReturnToMainMenu()
    {
        LevelLoader.instance.LoadLevel(0);
    }

    public void CloseWinScreen()
    {
        PlayMoveEffectUI();
    }

    IEnumerator SubscribeOnEnable()
    {
        yield return new WaitForSeconds(.1f);

        CO2Manager.Instance.OnCO2Over += PlayMoveEffectUI;
    }
}
