using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShakeElement : MonoBehaviour
{
    private Sequence sequence;

    [SerializeField]
    private RectTransform rectTransform;

    [Header("Shake Parameters")]

    [SerializeField]
    private float duration = 1, strength = 1;
    [SerializeField]
    private int vibrato = 10;
    [SerializeField]
    private float randomness = 90;
    [SerializeField]
    private bool fadeOut = true;
    [SerializeField]
    private ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Harmonic;

    private bool canPlayEffect = true;

    public void PlayShakeScale()
    {
        if (rectTransform == null)
            return;

        if (canPlayEffect == false)
            return;

        Debug.Log("Shake Scale");
        sequence.Kill();
        sequence = DOTween.Sequence()
            .Append(rectTransform.DOShakeScale(duration, strength, vibrato, randomness, fadeOut, randomnessMode));
        sequence.Play();
        canPlayEffect = false;
        StartCoroutine(KillSequence());
    }

    public void PlayShakeRotation()
    {
        if (rectTransform == null)
            return;

        if (canPlayEffect == false)
            return;

        Debug.Log("Shake Rotation");
        sequence.Kill();
        sequence = DOTween.Sequence()
            .Append(rectTransform.DOShakeRotation(duration, strength, vibrato, randomness, fadeOut, randomnessMode));
        sequence.Play();
        canPlayEffect = false;
        StartCoroutine(KillSequence());
    }

    IEnumerator KillSequence()
    {
        yield return new WaitForSeconds(duration + .2f);

        sequence.Kill();
        canPlayEffect = true;
    }
}
