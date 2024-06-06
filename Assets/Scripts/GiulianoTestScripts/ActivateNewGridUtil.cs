using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNewGridUtil : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer gridSR;

    private Sequence sequence;

    private void Awake()
    {
        gridSR = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UpgradeGridEventUtil.OnUpgradeGrid += ActivateGridSprite;
    }

    private void OnDisable()
    {
        UpgradeGridEventUtil.OnUpgradeGrid -= ActivateGridSprite;
    }

    private void ActivateGridSprite()
    {
        sequence.Kill();
        sequence = DOTween.Sequence()
            .Append(gridSR.DOFade(10, 10));
        sequence.Play();
        StartCoroutine(KillSequence());
    }

    IEnumerator KillSequence()
    {
        yield return new WaitForSeconds(11);
        sequence.Kill();
    }
}
