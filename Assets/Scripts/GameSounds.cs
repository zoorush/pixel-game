using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSounds : MonoBehaviour
{
    public static event System.Action<AudioClip, float> OnChoseSound;
    [SerializeField] private AudioSource _audioClip;
    void Start()
    {
        OnChoseSound += PlaySound;
    }

    public static void OnChoseSoundTrigger(AudioClip clip, float volume = 1)
    {
        OnChoseSound?.Invoke(clip, volume);
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        _audioClip.PlayOneShot(clip, volume);
    }
    private void OnDestroy()
    {
        OnChoseSound -= PlaySound;
    }
}
