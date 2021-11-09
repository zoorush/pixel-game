using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _frames;
    [SerializeField] private float _framesPerSecond = 8;
    private int _index = 0;
    

    private float _timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (_timer>1/ _framesPerSecond)
        {
            _timer = 0;
            _index++;
            _index %= _frames.Length;
            _image.sprite = _frames[_index];
        }
        _timer += Time.deltaTime;
    }
}
