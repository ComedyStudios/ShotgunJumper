using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ButtonController : MonoBehaviour
{
    public GameObject scrollView;
    public InputAction toggleInventoryAction;

    public void OnEnable()
    {
        toggleInventoryAction.Enable();
    }

    public void OnDisable()
    {
        toggleInventoryAction.Disable();
    }

    public void Awake()
    {
        toggleInventoryAction.performed += _ => ToggleScrollView();
    }

    public void ToggleScrollView()
    {
        if (scrollView.activeInHierarchy)
        {
            scrollView.SetActive(false);
        }
        else
        {
            scrollView.SetActive(true);
        }
    }
}
