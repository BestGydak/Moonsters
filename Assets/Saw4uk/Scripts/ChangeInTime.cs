using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TypeEasingFunctions
{
    Linear,
    EaseOutQuart
}

public class ChangeInTime : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> valueToChange;
    [SerializeField] private float timeToChange;
    [SerializeField] private TypeEasingFunctions typeEasingFunctions;
    [SerializeField] private Vector2 fromTo;
    [SerializeField] private bool infinity;

    private static readonly Dictionary<TypeEasingFunctions, Func<float, float>> functionDictionary =
        new()
        {
            {
                TypeEasingFunctions.Linear, LinearEasing
            },
            {
                TypeEasingFunctions.EaseOutQuart, EaseOutQuartEasing
            },
        };

    private Func<float, float> currentFunction;

    private void Awake()
    {
        currentFunction = functionDictionary[typeEasingFunctions];
        StartCoroutine(ChangeValueInTime());
    }

    private IEnumerator ChangeValueInTime()
    {
        var progress = 0f;

        while (progress < 1)
        {
            progress += Time.deltaTime / timeToChange;
            valueToChange.Invoke(Mathf.Lerp(fromTo.x, fromTo.y, currentFunction.Invoke(progress)));
            yield return null;
        }
    }

    private static float LinearEasing(float t)
    {
        return t;
    }

    private static float EaseOutQuartEasing(float t)
    {
        return 1 - Mathf.Pow(1 - t, 4);
    }
}