using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPanelOpener : MonoBehaviour
{
    [SerializeField] private Transform transform;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            transform.gameObject.SetActive(true);
    }
}
