using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    private PlayerManager _playerManager;
    
    //public List<Customization> Customizations;
    
    public Customization currentCustomization { get; private set; }

    public Customization[] Customizations;
    enum CustomizationCategories { HEAD, HAIR, FACE, BODY };

    private int _customizationCategoryIndex = System.Enum.GetValues(typeof(CustomizationCategories)).Length;


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

        // Temp way to delete saved data.
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            
            PlayerPrefs.DeleteKey("Head");
            PlayerPrefs.DeleteKey("Face");
            PlayerPrefs.DeleteKey("Hair");
        }

#if UNITY_EDITOR
        CustomizationSelection();
        CycleCustomizations();
        LoadCustomization();
#endif

    }

    private void CycleCustomizations()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        { 
            currentCustomization.NextMaterial();
            currentCustomization.NextSubObjects();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentCustomization.PreviousMaterial();
            currentCustomization.PreviousSubObjects();
        }
    }

    public void LoadCustomization()
    {

    if (Input.GetKeyDown(KeyCode.LeftShift))
    {
            LoadHead();
        currentCustomization.LoadCustomizationHead();
            LoadHair();
        currentCustomization.LoadCustomizationHair();
            LoadFace();
        currentCustomization.LoadCustomizationFace();
        }

    }

    public void LoadHead()
    {
        _customizationCategoryIndex = (int)CustomizationCategories.HEAD;
        currentCustomization = Customizations[_customizationCategoryIndex];
    }
    public void LoadHair()
    {
        _customizationCategoryIndex = (int)CustomizationCategories.HAIR;
        currentCustomization = Customizations[_customizationCategoryIndex];   
    }
    public void LoadFace()
    {
        _customizationCategoryIndex = (int)CustomizationCategories.FACE;
        currentCustomization = Customizations[_customizationCategoryIndex];
    }

    void CustomizationSelection()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
            _customizationCategoryIndex--;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            _customizationCategoryIndex--;
        if (_customizationCategoryIndex < 0)
            _customizationCategoryIndex = Customizations.Length - 1;
        if (_customizationCategoryIndex >= Customizations.Length)
            _customizationCategoryIndex = 0;
        currentCustomization = Customizations[_customizationCategoryIndex];

    }
}

[Serializable]
public class Customization
{

    public string DisplayName;

    public Renderer[] Renderers;
    public Material[] Materials;
    public GameObject[] SubObjects;

    private int _materialIndex;
    private int _subObjectIndex;


    public void NextMaterial()
    {

        _materialIndex++;
        if (_materialIndex >= Materials.Length)
            _materialIndex = 0;

        UpdateRenderers();
        SaveMaterialObjects();
    }

    public void PreviousMaterial()
    {

        _materialIndex--;
        if (_materialIndex < 0)
            _materialIndex = Materials.Length - 1;

        UpdateRenderers();
        SaveMaterialObjects();

    }

    public void UpdateRenderers()
    {

        foreach (var renderer in Renderers)
            if (renderer)
                renderer.material = Materials[_materialIndex];
    }

    public void SaveMaterialObjects()
    {
        PlayerPrefs.SetInt(DisplayName, _materialIndex);
        PlayerPrefs.Save();

        Debug.Log("Saved: " + DisplayName + " Item: " + _materialIndex);
    }

    public void NextSubObjects()
    {

        _subObjectIndex++;
        if (_subObjectIndex >= SubObjects.Length)
            _subObjectIndex = 0;

        UpdateSubObjects();
        SaveSubObjects();
    }

    public void PreviousSubObjects()
    {

        _subObjectIndex--;
        if (_subObjectIndex < 0)
            _subObjectIndex = SubObjects.Length - 1;

        UpdateSubObjects();
        SaveSubObjects();
    }

    public void UpdateSubObjects()
    {

        for (var i = 0; i < SubObjects.Length; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == _subObjectIndex);
    }

    // Save customization data to Player Prefs.
    public void SaveSubObjects()
    {
            PlayerPrefs.SetInt(DisplayName, _subObjectIndex);
            PlayerPrefs.Save();

            Debug.Log("Saved: " + DisplayName + " Item: " + _subObjectIndex);
    }


    public void LoadCustomizationHead()
    {

        int loadHead = PlayerPrefs.GetInt("Head");

        for (var i = 0; i < SubObjects.Length; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == loadHead);

        Debug.Log("Loaded: " + DisplayName + " Item: " + loadHead);
        
    }

    public void LoadCustomizationFace()
    {
        
        int loadFace = PlayerPrefs.GetInt("Face");
        
        for (var i = 0; i < SubObjects.Length; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == loadFace);
        
        Debug.Log("Loaded: " + DisplayName + " Item: " + loadFace);

    }
    
    public void LoadCustomizationHair()
    {
        
        int loadHair = PlayerPrefs.GetInt("Hair");
        
        for (var i = 0; i < SubObjects.Length; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == loadHair);
        
        Debug.Log("Loaded: " + DisplayName + " Item: " + loadHair);

    }

}