using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static event System.Action<bool> OnGameFinished;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _wonPanel;
    private void Awake()
    {
        OnGameFinished += GameFinished;
    }

    private void GameFinished(bool won)
    {
        if (!won)
        {
            _content.SetActive(true);
        }
        else
        {
            _wonPanel.SetActive(true);
        }
    }

    public static void OnGameFinishedTrigger(bool value)
    {
        OnGameFinished?.Invoke(value);
    }

    private void OnDestroy()
    {
        OnGameFinished -= GameFinished;
    }
}
