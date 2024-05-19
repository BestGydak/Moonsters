using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonsters
{
    public class OpenClose : MonoBehaviour
    {
        [SerializeField] private GameObject gameObject;
        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void OnButtonPressed(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                Toggle();
            }
        }
    }

}