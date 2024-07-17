using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject itemsMenuCanvas;
    [SerializeField] private GameObject arPositionCanvas;
    [SerializeField] private GameObject saveMenuCanvas; // New Canvas
    [SerializeField] private GameObject saveConfirmationCanvas; // New Canvas
    [SerializeField] private GameObject loadConfirmationCanvas; // New Canvas

    public static UIManager instance;

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

    private void Start()
    {
        GameManager.instance.OnMainMenu += ActivateMainMenu;
        GameManager.instance.OnItemsMenu += ActivateItemsMenu;
        GameManager.instance.OnARPosition += ActivateARPosition;
    }

    private void ActivateMainMenu()
    {
        mainMenuCanvas.SetActive(true); // Ensure the Main Menu Canvas is active before scaling
        for (int i = 0; i < mainMenuCanvas.transform.childCount; i++)
        {
            mainMenuCanvas.transform.GetChild(i).DOScale(new Vector3(1, 1, 1), 0.3f);
        }

        for (int i = 0; i < itemsMenuCanvas.transform.childCount; i++)
        {
            itemsMenuCanvas.transform.GetChild(i).DOScale(new Vector3(0, 0, 0), 0.3f);
        }

        for (int i = 0; i < arPositionCanvas.transform.childCount; i++)
        {
            arPositionCanvas.transform.GetChild(i).DOScale(new Vector3(0, 0, 0), 0.3f);
        }

        saveMenuCanvas.SetActive(false); // Hide save menu
        saveConfirmationCanvas.SetActive(false); // Hide save confirmation
        loadConfirmationCanvas.SetActive(false); // Hide load confirmation

        Debug.Log("Main Menu Activated");
    }

    private void ActivateItemsMenu()
    {
        mainMenuCanvas.SetActive(false); // Ensure the Main Menu Canvas is hidden
        for (int i = 0; i < itemsMenuCanvas.transform.childCount; i++)
        {
            itemsMenuCanvas.transform.GetChild(i).DOScale(new Vector3(1, 1, 1), 0.5f);
        }

        itemsMenuCanvas.SetActive(true); // Ensure the Items Menu Canvas is active
        Debug.Log("Items Menu Activated");
    }

    private void ActivateARPosition()
    {
        mainMenuCanvas.SetActive(false); // Ensure the Main Menu Canvas is hidden
        for (int i = 0; i < mainMenuCanvas.transform.childCount; i++)
        {
            mainMenuCanvas.transform.GetChild(i).DOScale(new Vector3(0, 0, 0), 0.3f);
        }

        for (int i = 0; i < itemsMenuCanvas.transform.childCount; i++)
        {
            itemsMenuCanvas.transform.GetChild(i).DOScale(new Vector3(0, 0, 0), 0.3f);
        }

        itemsMenuCanvas.SetActive(false); // Ensure the Items Menu Canvas is hidden

        for (int i = 0; i < arPositionCanvas.transform.childCount; i++)
        {
            arPositionCanvas.transform.GetChild(i).DOScale(new Vector3(1, 1, 1), 0.3f);
        }

        arPositionCanvas.SetActive(true); // Ensure the AR Position Canvas is active
        Debug.Log("AR Position Menu Activated");
    }

    public void ActivateSaveMenuCanvas()
    {
        mainMenuCanvas.SetActive(false);
        itemsMenuCanvas.SetActive(false);
        arPositionCanvas.SetActive(false);
        saveMenuCanvas.SetActive(true);
        Debug.Log("Save Menu Canvas Activated");
    }

    public void ActivateSaveConfirmationCanvas()
    {
        saveMenuCanvas.SetActive(false);
        saveConfirmationCanvas.SetActive(true);
        Debug.Log("Save Confirmation Canvas Activated");
    }

    public void ActivateLoadConfirmationCanvas()
    {
        saveMenuCanvas.SetActive(false);
        loadConfirmationCanvas.SetActive(true);
        Debug.Log("Load Confirmation Canvas Activated");
    }

    public void ActivateMainMenuFromSave()
    {
        saveConfirmationCanvas.SetActive(false);
        loadConfirmationCanvas.SetActive(false);
        ActivateMainMenu();
        Debug.Log("Returning to Main Menu from Save/Load");
    }
}






