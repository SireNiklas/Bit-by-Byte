using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCustomizerUI : MonoBehaviour
{

    public CharacterCustomization characterCustomization;
    Customization currentCustomization;

    //PlayerMovementController playerMovementController;
    //PlayerActionsController playerActionsController;
    //PlayerStats playerStats;

    [SerializeField]private GameObject player;


    private void Awake()
    {

    }


    private void Start()
    {

        currentCustomization = new Customization();

    }

    public void NextButtonPressed()
    {

        characterCustomization.currentCustomization.NextSubObjects();

    }
    public void PreviousButtonPressed()
    {

        characterCustomization.currentCustomization.PreviousSubObjects();

    }

    public void PlayGame()
    {

        // Calls camera which doesn't exist.
        player.GetComponent<PlayerMovementController>().enabled = true;
        player.GetComponent<PlayerActionsController>().enabled = true;
        player.GetComponent<PlayerStats>().enabled = true;
        player.GetComponent<CharacterCustomizationInitialization>().enabled = true;

        SceneManager.LoadScene("Protoworld");

    }

    // Temp name - Change it.
    public void LoadCustom()
    {

        characterCustomization.LoadHead();
        characterCustomization.currentCustomization.LoadCustomizationHead();
        characterCustomization.LoadHair();
        characterCustomization.currentCustomization.LoadCustomizationHair();
        characterCustomization.LoadFace();
        characterCustomization.currentCustomization.LoadCustomizationFace();

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
