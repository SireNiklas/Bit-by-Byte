using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationInitialization : MonoBehaviour
{

    public CharacterCustomization characterCustomization;

    private void Awake()
    {

        characterCustomization = GetComponent<CharacterCustomization>();

        characterCustomization.LoadHead();
        characterCustomization.currentCustomization.LoadCustomizationHead();
        characterCustomization.LoadHair();
        characterCustomization.currentCustomization.LoadCustomizationHair();
        characterCustomization.LoadFace();
        characterCustomization.currentCustomization.LoadCustomizationFace();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
