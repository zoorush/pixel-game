using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneButton : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Game";
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
