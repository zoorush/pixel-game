using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{

    void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(ChangeValue);
        GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("audio_mute", 0) == 1;
    }

    private void ChangeValue(bool mute)
    {
        AudioListener.pause = mute;
    }

}
