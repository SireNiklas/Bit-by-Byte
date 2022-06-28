using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizerUI : MonoBehaviour
{

    public CharacterCustomization characterCustomization;
    Customization currentCustomization;

    private void Start()
    {

        currentCustomization = new Customization();

    }

    public void NextButtonPressed()
    {

        currentCustomization.NextSubObjects();

    }
    public void PreviousButtonPressed()
    {

        currentCustomization.PreviousSubObjects();

    }

    public void Head()
    {
        characterCustomization.LoadHead();
    }
    public void Hair()
    {
        characterCustomization.LoadHair();
    }
    public void Face()
    {
        characterCustomization.LoadFace();
    }
}
