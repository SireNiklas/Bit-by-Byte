using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationInitialization : MonoBehaviour
{

    public CharacterCustomization characterCustomization;
    [SerializeField] private GameObject player;


    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Start is called before the first frame update
    void Start()
    {

        player.GetComponent<PlayerMovementController>().enabled = true;
        player.GetComponent<PlayerStats>().enabled = true;
        player.GetComponent<PlayerActionsController>().enabled = true;
        player.GetComponent<CharacterCustomizationInitialization>().enabled = true;

        characterCustomization = GetComponent<CharacterCustomization>();

        characterCustomization.LoadHead();
        characterCustomization.currentCustomization.LoadCustomizationHead();
        characterCustomization.LoadHair();
        characterCustomization.currentCustomization.LoadCustomizationHair();
        characterCustomization.LoadFace();
        characterCustomization.currentCustomization.LoadCustomizationFace();
        characterCustomization.LoadBody();
        characterCustomization.currentCustomization.LoadCustomizationBody();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
