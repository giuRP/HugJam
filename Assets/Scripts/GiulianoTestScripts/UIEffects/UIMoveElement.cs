using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveElement : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;

    private Vector3 startPosition;

    [SerializeField]
    private Vector3 endPosition;

    private Sequence sequence;

    private void Awake()
    {
        startPosition = rectTransform.position;
    }

    public void PlayPositiveMovement()
    {
        Vector3 endPos = new Vector3(startPosition.x, endPosition.y, startPosition.z);

        sequence.Kill();

        sequence = DOTween.Sequence()
            .Append(rectTransform.DOMove(endPos, 2));
        sequence.Play();
        //StartCoroutine(KillSequence());
    }

    public void PlayNegativeMovement()
    {
        Vector3 endPos = new Vector3(startPosition.x, -endPosition.y, startPosition.z);

        sequence.Kill();

        sequence = DOTween.Sequence()
            .Append(rectTransform.DOMove(startPosition, 2));
        sequence.Play();
        StartCoroutine(KillSequence());
    }

    IEnumerator KillSequence()
    {
        yield return new WaitForSeconds(2f);
        sequence.Kill();
    }
}
