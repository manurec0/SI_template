using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.2f;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }

    public static IEnumerator PitchUp(AudioSource audioSource, float threshold, float FadeTime)
    {
        float startPitch = 1f;
        while (audioSource.pitch < threshold)
        {
            audioSource.pitch += startPitch * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.pitch = startPitch;
    }

    public static void PitchDown(AudioSource audioSource, float threshold, float factor)
    {
        while (audioSource.pitch > threshold)
        {
            audioSource.pitch -= factor;
        }

        audioSource.pitch = 1f;
    }
}