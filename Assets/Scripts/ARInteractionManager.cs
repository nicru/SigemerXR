using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInteractionManager : MonoBehaviour
{
    [SerializeField] private Camera aRCamera;
    private ARRaycastManager aRRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private List<GameObject> placedModels = new List<GameObject>();

    private GameObject aRPointer;
    private GameObject item3DModel;
    private GameObject itemSelected;
    private bool isInitialPosition;
    private bool isOverUI;
    private bool isOver3DModel;

    private Vector2 initialTouchPosition;
    private float initialDistance;
    private Vector3 initialScale;
    private Vector3 minScale;
    private Vector3 maxScale;

    public GameObject Item3DModel
    {
        set
        {
            item3DModel = value;
            item3DModel.transform.position = aRPointer.transform.position;
            item3DModel.transform.parent = aRPointer.transform;
            isInitialPosition = true; // Set initial position
            LookAtCamera(item3DModel); // Make the item look at the camera initially
            placedModels.Add(item3DModel); // Add to placedModels list

            // Set scale boundaries based on the original size
            initialScale = item3DModel.transform.localScale;
            minScale = initialScale * 0.5f; // Minimum scale is half the original size
            maxScale = initialScale * 2.0f; // Maximum scale is twice the original size
        }
    }

    void Start()
    {
        aRPointer = transform.GetChild(0).gameObject;
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        GameManager.instance.OnMainMenu += SetItemPosition;
    }

    void Update()
    {
        if (isInitialPosition)
        {
            Vector2 middlePointScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            aRRaycastManager.Raycast(middlePointScreen, hits, TrackableType.Planes); // Perform raycast
            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position; // Set position
                transform.rotation = hits[0].pose.rotation; // Set rotation
                aRPointer.SetActive(true);
                isInitialPosition = false;

                LookAtCamera(item3DModel); // Ensure the item faces the camera after setting its initial position
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touchOne = Input.GetTouch(0); // Get the first touch
            if (touchOne.phase == TouchPhase.Began)
            {
                var touchPosition = touchOne.position;
                isOverUI = isTapOverUI(touchPosition);
                isOver3DModel = isTapOver3DModel(touchPosition);
            }

            if (touchOne.phase == TouchPhase.Moved) // Check if the finger is moving on the screen
            {
                if (aRRaycastManager.Raycast(touchOne.position, hits, TrackableType.Planes))
                {
                    Pose hitPose = hits[0].pose;
                    if (!isOverUI && isOver3DModel && item3DModel != null) // Check if not over UI and over 3D model
                    {
                        item3DModel.transform.position = hitPose.position; // Set new position for 3D model
                        aRPointer.transform.position = hitPose.position; // Ensure the AR pointer moves with the model
                    }
                }
            }

            // This condition handles rotating the 3D model
            if (Input.touchCount == 2 && item3DModel != null) // Check if there are 2 inputs
            {
                Touch touchTwo = Input.GetTouch(1); // Get the second touch
                if (touchOne.phase == TouchPhase.Began || touchTwo.phase == TouchPhase.Began) // Check if either touch has started
                {
                    initialTouchPosition = touchTwo.position - touchOne.position; // Set the initial touch position
                    initialDistance = Vector2.Distance(touchOne.position, touchTwo.position);
                    initialScale = item3DModel.transform.localScale;
                }

                if (touchOne.phase == TouchPhase.Moved || touchTwo.phase == TouchPhase.Moved) // Check if either touch has moved
                {
                    Vector2 currentTouchPosition = touchTwo.position - touchOne.position; // Set the current touch position
                    float rotationAngle = Vector2.SignedAngle(initialTouchPosition, currentTouchPosition); // Get the rotation angle
                    item3DModel.transform.rotation = Quaternion.Euler(0, item3DModel.transform.eulerAngles.y - rotationAngle, 0); // Set the rotation of the 3D model
                    initialTouchPosition = currentTouchPosition; // Set the initial touch position

                    // Scaling logic
                    float currentDistance = Vector2.Distance(touchOne.position, touchTwo.position);
                    float scaleMultiplier = currentDistance / initialDistance;
                    Vector3 newScale = initialScale * scaleMultiplier;
                    newScale = ClampScale(newScale);
                    item3DModel.transform.localScale = newScale;
                }
            }

            // This condition allows selecting and moving already installed models
            if (isOver3DModel && item3DModel == null && !isOverUI) // Check if over a 3D model, not over UI and no model selected
            {
                GameManager.instance.ARPosition(); // Call the ARPosition method from the GameManager
                item3DModel = itemSelected; // Set the selected item as the 3D model
                itemSelected = null; // Clear the selected item
                aRPointer.SetActive(true); // Activate the pointer
                transform.position = item3DModel.transform.position; // Set the position of the 3D model
                item3DModel.transform.parent = aRPointer.transform; // Set the 3D model as a child of the pointer to move it
                aRPointer.transform.position = item3DModel.transform.position; // Ensure the AR pointer moves with the model
            }
        }

        // Clean up null references from the placedModels list
        for (int i = placedModels.Count - 1; i >= 0; i--)
        {
            if (placedModels[i] == null)
            {
                placedModels.RemoveAt(i);
            }
        }
    } // <--- End of Update method

    // This method allows rotating the 3D model to face the camera
    private void LookAtCamera(GameObject obj)
    {
        if (obj != null)
        {
            Vector3 lookPos = aRCamera.transform.position - obj.transform.position;
            lookPos.y = 0; // Keep the object upright
            obj.transform.rotation = Quaternion.LookRotation(lookPos);
        }
    }

    private Vector3 ClampScale(Vector3 scale)
    {
        scale.x = Mathf.Clamp(scale.x, minScale.x, maxScale.x);
        scale.y = Mathf.Clamp(scale.y, minScale.y, maxScale.y);
        scale.z = Mathf.Clamp(scale.z, minScale.z, maxScale.z);
        return scale;
    }

    // This method checks if the tap was over the 3D model
    private bool isTapOver3DModel(Vector2 touchPosition)
    {
        Ray ray = aRCamera.ScreenPointToRay(touchPosition); // Create a ray from the camera to the touch position
        if (Physics.Raycast(ray, out RaycastHit hit3DModel)) // Check if the ray hit an object
        {
            if (hit3DModel.collider.CompareTag("Item")) // Check if the object has the tag "Item"
            {
                itemSelected = hit3DModel.transform.gameObject; // Set the hit object as the selected item
                return true;
            }
        }

        return false;
    }

    private bool isTapOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current); // Create a PointerEventData object
        eventData.position = new Vector2(touchPosition.x, touchPosition.y); // Set the position of the event

        List<RaycastResult> results = new List<RaycastResult>(); // Create a list for raycast results
        EventSystem.current.RaycastAll(eventData, results); // Perform a raycast on all UI elements

        return results.Count > 0; // Return if any results were found
    }

    // This method sets the item position
    private void SetItemPosition()
    {
        if (item3DModel != null)
        {
            item3DModel.transform.parent = null;
            aRPointer.SetActive(false);
            item3DModel = null;
        }
    }

    // This method allows deleting an instance of a 3D model
    public void DeleteItem()
    {
        if (item3DModel != null)
        {
            placedModels.Remove(item3DModel); // Remove the model from the list
            Destroy(item3DModel); // Destroy the model
            aRPointer.SetActive(false);
            GameManager.instance.MainMenu();
        }
    }
}
