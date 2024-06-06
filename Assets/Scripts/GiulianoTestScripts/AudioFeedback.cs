using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeedback : MonoBehaviour
{
    public AudioClip clip;

    public List<AudioClip> clipList;

    public AudioSource targetAudioSource;

    [Range(0, 1)]
    public float volume = 1;

    public void PlayClip()
    {
        //if (clip == null)
        //    return;

        targetAudioSource.volume = this.volume;
        targetAudioSource.PlayOneShot(ChoseRandomClip());
    }

    private AudioClip ChoseRandomClip()
    {
        if(clipList.Count > 0)
        {
            int index = Random.Range(0, clipList.Count);
            return clipList[index];
        }
        else
        {
            return clip;
        }
    }

    public void PlaySpecificClip(AudioClip clipToPlay = null)
    {
        if (clipToPlay == null)
            clipToPlay = clip;

        if (clipToPlay == null)
            return;

        targetAudioSource.volume = this.volume;
        targetAudioSource.PlayOneShot(clipToPlay);
    }
}
