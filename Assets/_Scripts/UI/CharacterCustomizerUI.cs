using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Managers.Game;

public class CharacterCustomizerUI : MonoBehaviour
{

    public CharacterCustomization characterCustomization;
    Customization currentCustomization;

    //PlayerMovementController playerMovementController;
    //PlayerActionsController playerActionsController;
    //PlayerStats playerStats;

    [SerializeField]private GameObject player;
    [SerializeField] private GameObject _playerHud;

    private void Awake()
    {

    }


    private void Start()
    {

        currentCustomization = new Customization();
    }

    public void NextButtonPressed()
    {

        if (characterCustomization._customizationCategoryIndex == 3)
            characterCustomization.currentCustomization.NextMaterial();
        else if (characterCustomization._customizationCategoryIndex != 3)
            characterCustomization.currentCustomization.NextSubObjects();

    }
    public void PreviousButtonPressed()
    {
        if (characterCustomization._customizationCategoryIndex == 3)
            characterCustomization.currentCustomization.PreviousMaterial();
        else if (characterCustomization._customizationCategoryIndex != 3)
            characterCustomization.currentCustomization.PreviousSubObjects();

    }

    public void PlayGame()
    {

        Managers.PlayerManager.Instance.EnablePlayerComponents(true);

        Managers.Game.GameManager.Instance.LockCurser();

        this.gameObject.SetActive(false);
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
        characterCustomization.LoadBody();
        characterCustomization.currentCustomization.LoadCustomizationBody();

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
    public void Body()
    {
        characterCustomization.LoadBody();
    }
}
