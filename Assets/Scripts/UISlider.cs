using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetFillRate(float value)
    {
        _image.fillAmount = value;
    }
}
