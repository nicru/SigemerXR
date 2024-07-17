using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ARSessionManager : MonoBehaviour
{
    public static ARSessionManager instance;
    private string savePath;

    [System.Serializable]
    public class PrefabData
    {
        public string prefabName;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale; // Add scale to save and load the scale of the model
    }

    [System.Serializable]
    public class SessionData
    {
        public List<PrefabData> prefabs = new List<PrefabData>();
    }

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

        savePath = Path.Combine(Application.persistentDataPath, "sessionData.json");
    }

    public void SaveData()
    {
        SessionData sessionData = new SessionData();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Item"))
        {
            string prefabName = obj.name;
            if (prefabName.EndsWith("(Clone)"))
            {
                prefabName = prefabName.Substring(0, prefabName.Length - 7);
            }

            PrefabData prefabData = new PrefabData
            {
                prefabName = prefabName,
                position = obj.transform.position,
                rotation = obj.transform.rotation,
                scale = obj.transform.localScale // Save the scale of the model
            };
            sessionData.prefabs.Add(prefabData);
            Debug.Log("Saving prefab: " + prefabName + " at position: " + obj.transform.position + " with scale: " + obj.transform.localScale);
        }

        string json = JsonUtility.ToJson(sessionData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Session Data Saved to " + savePath);
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SessionData sessionData = JsonUtility.FromJson<SessionData>(json);

            Debug.Log("Session Data Loaded: " + json);

            foreach (PrefabData prefabData in sessionData.prefabs)
            {
                Debug.Log("Loading prefab: " + prefabData.prefabName + " at position: " + prefabData.position + " with scale: " + prefabData.scale);
                GameObject prefab = Resources.Load<GameObject>(prefabData.prefabName);
                if (prefab == null) 
                {
                    Debug.LogError("Prefab not found: " + prefabData.prefabName);
                } 
                else 
                {
                    Debug.Log("Prefab loaded: " + prefabData.prefabName);
                    GameObject instance = Instantiate(prefab, prefabData.position, prefabData.rotation);
                    instance.transform.localScale = prefabData.scale; // Load the scale of the model
                    instance.tag = "Item";
                    Debug.Log("Instantiated prefab: " + prefabData.prefabName + " at position: " + prefabData.position + " with scale: " + prefabData.scale);
                }
            }

            Debug.Log("Session Data Loaded Successfully");
        }
        else
        {
            Debug.LogWarning("No session data found to load.");
        }
    }
}




