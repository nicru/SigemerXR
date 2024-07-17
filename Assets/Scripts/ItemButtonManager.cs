using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonManager : MonoBehaviour
{
    // Crea atributos utilizados por DataManager para armar el botón
    private string itemName;
    private string itemDescription;
    private Sprite itemImage;
    private GameObject item3DModel;
    private ARInteractionManager interactionManager;

    public string ItemName {
        set
        {
            itemName = value;
        }
    }

    public string ItemDescription { set => itemDescription = value; }
    public Sprite ItemImage { set => itemImage = value; }
    public GameObject Item3DModel { set => item3DModel = value; }
    // En Start se asignan los valores a los elementos del botón
    // Se asigna el nombre, la imagen y la descripción
    // Se agrega un Listener al botón que llama a ARPosition y Create3DModel
    void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = itemName;
        transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
        transform.GetChild(2).GetComponent<Text>().text = itemDescription;
        
        var button = GetComponent<Button>();
        button.onClick.AddListener(GameManager.instance.ARPosition);
        button.onClick.AddListener(Create3DModel);

        interactionManager = FindObjectOfType<ARInteractionManager>();
    }

    // Crea el modelo 3D y lo asigna en ARInteractionManager
    private void Create3DModel()
    {
        interactionManager.Item3DModel = Instantiate(item3DModel);
    }

}
