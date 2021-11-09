using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIValues : MonoBehaviour
{
    [SerializeField] private TMP_Text _yardsCounterText;
    [SerializeField] private UISlider _speedBonus;
    [SerializeField] private TMP_Text _nextTDText;
    [SerializeField] private TMP_Text _attemptText;
    [SerializeField] private TMP_Text _turnCountText;
    [SerializeField] private TMP_Text _jumpCountText;
    [SerializeField] private TMP_Text _coinCount;

    private void Awake()
    {
        int baseSize = 100;
        baseSize += GameData.InsreasePerLevel * GameData.CurrentLevel;
        NextTouchDown(baseSize);

        //_attemptText.text = "Down: " + GameData.CurrentAttempt;
        SetCoinCount(0);
    }

    public void NextTouchDown(int touchdown)
    {
        _nextTDText.text = "Next TD: " + touchdown;
    }

    public void SetCounterText(string text)
    {
        _yardsCounterText.text = text;
    }

    public void SetSpeedSlider(float value)
    {
        _speedBonus.SetFillRate(value);
    }

    public void SetTurnCount(int value)
    {
        _turnCountText.text = value.ToString();
    }
    public void SetJumpCount(int value)
    {
        _jumpCountText.text = value.ToString();
    }
    public void SetCoinCount(int value)
    {
        _coinCount.text = value.ToString();
    }
}
