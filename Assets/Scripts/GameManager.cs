// GameManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action OnMainMenu;
    public event Action OnItemsMenu;
    public event Action OnARPosition;

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        OnMainMenu?.Invoke();
        Debug.Log("Main Menu Activated");
    }

    public void ItemsMenu()
    {
        OnItemsMenu?.Invoke();
        Debug.Log("Items Menu Activated");
    }

    public void ARPosition()
    {
        OnARPosition?.Invoke();
        Debug.Log("AR Position Activated");
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void SaveSession()
    {
        ARSessionManager.instance.SaveData();
        Debug.Log("Session Saved");
    }

    public void LoadSession()
    {
        ARSessionManager.instance.LoadData();
        Debug.Log("Session Loaded");
    }

    public void ShowSaveConfirmation()
    {
        UIManager.instance.ActivateSaveConfirmationCanvas();
        Debug.Log("Showing Save Confirmation Canvas");
    }

    public void ShowLoadConfirmation()
    {
        UIManager.instance.ActivateLoadConfirmationCanvas();
        Debug.Log("Showing Load Confirmation Canvas");
    }

    public void ConfirmSave()
    {
        SaveSession();
        UIManager.instance.ActivateMainMenuFromSave();
        Debug.Log("Save Confirmed, Returning to Main Menu");
    }

    public void ConfirmLoad()
    {
        LoadSession();
        UIManager.instance.ActivateMainMenuFromSave();
        Debug.Log("Load Confirmed, Returning to Main Menu");
    }
}


