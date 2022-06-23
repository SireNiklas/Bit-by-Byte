using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CharacterCustomization : MonoBehaviour
{
    private PlayerManager _playerManager;
    
    public List<Customization> Customizations;
    private int _currentCustomizationIndex;

    public Customization CurrentCustomization { get; private set; }

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
        
        SelectCustomization();
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            CurrentCustomization.NextMaterial();
            Debug.Log("RIGHT ARROW");
            CurrentCustomization.NextSubObjects();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {

            SceneManager.LoadScene("ProtoWorld");
        }
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
        CurrentCustomization = Customizations[_currentCustomizationIndex];

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


}