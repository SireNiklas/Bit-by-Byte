using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    private PlayerManager _playerManager;
    
    public List<Customization> Customizations;
    public int _currentCustomizationIndex;
    
    public Customization currentCustomization { get; private set; }
    //enum CustomizationCategories { Head, Hair, Face };

    private void Awake()
    {

        foreach (var customization in Customizations)
        {
            
            customization.UpdateRenderers();
            customization.UpdateSubObjects();
            
        }        
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            currentCustomization.NextMaterial();
            Debug.Log("RIGHT ARROW");
            currentCustomization.NextSubObjects();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentCustomization.PreviousSubObjects();

        // Load
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            Debug.Log("Loaded");

            LoadHead();
            currentCustomization.LoadCustomizationHead();

            LoadHair();
            currentCustomization.LoadCustomizationHair();

            LoadFace();
            currentCustomization.LoadCustomizationFace();

        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            
            PlayerPrefs.DeleteKey("Head");
            PlayerPrefs.DeleteKey("Face");
            PlayerPrefs.DeleteKey("Hair");

        }
    
        SelectCustomization();
        
    }

    public void LoadHead()
    {
        _currentCustomizationIndex = 0;
        currentCustomization = Customizations[_currentCustomizationIndex];
    }
    public void LoadHair()
    {
        _currentCustomizationIndex = 1;
        currentCustomization = Customizations[_currentCustomizationIndex];   
    }
    public void LoadFace()
    {
        _currentCustomizationIndex = 2;
        currentCustomization = Customizations[_currentCustomizationIndex];
    }

    void SelectCustomization()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
            _currentCustomizationIndex++;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            _currentCustomizationIndex--;
        if (_currentCustomizationIndex < 0)
            _currentCustomizationIndex = Customizations.Count - 1;
        if (_currentCustomizationIndex >= Customizations.Count)
            _currentCustomizationIndex = 0;
        currentCustomization = Customizations[_currentCustomizationIndex];

    }
}

[Serializable]
public class Customization
{

    public string DisplayName;

    public List<Renderer> Renderers;
    public List<Material> Materials;
    public List<GameObject> SubObjects;

    private int _materialIndex;
    private int _subObjectIndex;


    public void NextMaterial()
    {

        _materialIndex++;
        if (_materialIndex >= Materials.Count)
            _materialIndex = 0;

    }
    
    public void NextSubObjects()
    {

        _subObjectIndex++;
        if (_subObjectIndex >= SubObjects.Count)
            _subObjectIndex = 0;

        UpdateSubObjects();
        SaveCustomization();
    }

    public void PreviousSubObjects()
    {

        _subObjectIndex--;
        if (_subObjectIndex < 0)
            _subObjectIndex = SubObjects.Count - 1;

        UpdateSubObjects();
        SaveCustomization();
    }

    public void UpdateSubObjects()
    {

        for (var i = 0; i < SubObjects.Count; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == _subObjectIndex);
        
    }

    public void UpdateRenderers()
    {

        foreach (var renderer in Renderers)
            if (renderer)
                renderer.material = Materials[_materialIndex];
    }

    public void SaveCustomization()
    {
        PlayerPrefs.SetInt(DisplayName, _subObjectIndex);
        PlayerPrefs.Save();

        Debug.Log("Saved: " + DisplayName + " Item: " + _subObjectIndex);
    }
    
    public void Load()
    {
        
        int loadHead = PlayerPrefs.GetInt("Head");
        int loadFace = PlayerPrefs.GetInt("Face");
        int loadHair = PlayerPrefs.GetInt("Hair");
        
        
        
        for (var i = 0; i < SubObjects.Count; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == loadHead);
        for (var i = 0; i < SubObjects.Count; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == loadFace);
        for (var i = 0; i < SubObjects.Count; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == loadHair);

        Debug.Log("Loaded: " + DisplayName + " Item: " + loadHead);
        
    }

    public void LoadCustomizationHead()
    {

        int loadHead = PlayerPrefs.GetInt("Head");

        for (var i = 0; i < SubObjects.Count; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == loadHead);

        Debug.Log("Loaded: " + DisplayName + " Item: " + loadHead);
        
    }

    public void LoadCustomizationFace()
    {
        
        int loadFace = PlayerPrefs.GetInt("Face");
        
        for (var i = 0; i < SubObjects.Count; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == loadFace);
        
        Debug.Log("Loaded: " + DisplayName + " Item: " + loadFace);

    }
    
    public void LoadCustomizationHair()
    {
        
        int loadHair = PlayerPrefs.GetInt("Hair");
        
        for (var i = 0; i < SubObjects.Count; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == loadHair);
        
        Debug.Log("Loaded: " + DisplayName + " Item: " + loadHair);

    }

}