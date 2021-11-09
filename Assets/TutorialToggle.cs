using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialToggle : MonoBehaviour
{

    void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(ChangeValue);
        GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("tutorial", 1) != 1;
    }

    private void ChangeValue(bool on)
    {
        PlayerPrefs.SetInt("tutorial", on ? 0 : 1);
    }

}
