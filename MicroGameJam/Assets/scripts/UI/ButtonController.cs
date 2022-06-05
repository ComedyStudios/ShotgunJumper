
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject scrollView;
    public InputAction toggleInventoryAction;
    public String startScene;

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

    public void TryAgain()
    {
        SceneManager.LoadScene(startScene);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
