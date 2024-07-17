// Asigna y crea un botón por cada item que exista.
// Cada botón tiene un texto que muestra el nombre del item, el ícono y la descripción.
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private ItemButtonManager itemButtonManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.OnItemsMenu += CreateButtons;
    }

    // Ojo con estas declaraciones
    // Uso de mayuscula y minuscula que puede interferir con el funcionamiento
    private void CreateButtons()
    {
        foreach ( var item in items)
        {
            ItemButtonManager itemButton;
            itemButton = Instantiate(itemButtonManager, buttonContainer.transform);
            itemButton.ItemName = item.itemName;
            itemButton.ItemDescription = item.ItemDescription;
            itemButton.ItemImage = item.itemImage;
            itemButton.Item3DModel = item.item3DModel;
            itemButton.name = item.itemName;
        }

        GameManager.instance.OnItemsMenu -= CreateButtons;
    }

}
