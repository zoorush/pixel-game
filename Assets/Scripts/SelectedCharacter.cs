using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedCharacter : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    void Start()
    {
        _dropdown.value = GameData.SelectedCharacter;
        _dropdown.onValueChanged.AddListener(ChooseCharacter);
    }

    private void ChooseCharacter(int index)
    {
        GameData.SelectedCharacter = index;
    }

}
