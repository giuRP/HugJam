using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioFeedbackUtil : MonoBehaviour
{
    [SerializeField]
    AudioFeedback audioFeedback;

    private void Awake()
    {
        audioFeedback = GetComponentInChildren<AudioFeedback>();
    }

    private void OnEnable()
    {
        AddCO2EventUtil.OnAddCO2 += PlayAudioFeedback;  
    }

    private void OnDisable()
    {
        AddCO2EventUtil.OnAddCO2 -= PlayAudioFeedback;
    }

    private void PlayAudioFeedback()
    {
        audioFeedback.PlayClip();
    }
}
