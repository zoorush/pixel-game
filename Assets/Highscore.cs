using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Vector3 _scale = Vector3.one;

    private void OnEnable()
    {
        _text.text = GameData.Highscore.ToString();
        GameData.PublishHighscore();
    }

    private void Update()
    {
        _scale.x = _scale.y = _scale.z =1 + Mathf.PingPong(Time.time / 2f,.5f);
        transform.localScale = _scale;
    }
}
