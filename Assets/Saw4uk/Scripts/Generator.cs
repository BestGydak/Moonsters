using System;
using System.Collections;
using System.Collections.Generic;
using Moonsters;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    [SerializeField] private GeneratorField generatorField;
    [SerializeField] private GeneratorField actionField;
    [SerializeField] private GameObject helpGameObject;

    [Header("Settings")] 
    [SerializeField] private float secondsToLoad;

    [SerializeField] private Slider slider;
    
    private bool isActive;
    private bool canActivate;
    private bool isActivated;
    private float value;
    
    private AstronautModel astronautModel;
    private void Awake()
    {
        generatorField.astronautChanged += OnAstroChanged;
        actionField.astronautChanged += OnActionAstroChanged;
        slider.maxValue = 1;
        slider.minValue = 0;
        slider.value = 0;
        slider.gameObject.SetActive(false);
    }

    private void OnActionAstroChanged(AstronautModel astro)
    {
        if(isActivated || isActive)
            return;
        canActivate = true;
        helpGameObject.SetActive(astro);
    }
    
    private void OnAstroChanged(AstronautModel obj)
    {
        if(isActivated) return;

        if (!isActive && value > 0)
        {
            StartCoroutine(LoadingCoroutine());
        }
        
        if (obj != null)
        {
            astronautModel = obj;
            astronautModel.SetGenerator(this);
        }
        else
        {
            astronautModel.SetGenerator(null);
            OnCharacterExit();
            astronautModel = obj;
            
        }
    }

    public void StartAction()
    {
        if (canActivate)
        {
            StopAllCoroutines();
            StartCoroutine(LoadingCoroutine());
        }
            
    }

    private void OnCharacterExit()
    {
        if (!isActivated)
        {
            StopAllCoroutines();
            canActivate = false;
            isActive = false;
        }
    }
    
    private IEnumerator LoadingCoroutine()
    {
        isActive = true;
        helpGameObject.gameObject.SetActive(false);
        slider.gameObject.SetActive(true);
        while (value < 1)
        {
            value += Time.deltaTime / secondsToLoad;
            slider.value = value;
            yield return null;
        }

        isActivated = true;
        canActivate = false;
        isActive = false;
    }
}
