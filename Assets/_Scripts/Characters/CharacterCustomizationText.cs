using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterCustomizationText : MonoBehaviour
{

    [SerializeField] TMP_Text _text;
    [SerializeField] CharacterCustomization _characterCustomization;

    void OnValidate()
    {
        _text = GetComponent<TMP_Text>();
        _characterCustomization = FindObjectOfType<CharacterCustomization>();
    }

    private void Update()
    {
        _text.SetText(_characterCustomization.currentCustomization?.DisplayName);
    }
}