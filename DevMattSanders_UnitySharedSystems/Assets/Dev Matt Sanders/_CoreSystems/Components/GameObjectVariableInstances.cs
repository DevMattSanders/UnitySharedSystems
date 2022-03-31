using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
/*
public class GameObjectVariableInstances : SerializedMonoBehaviour
{
   // private static GameObjectVariableInstances _instance;
    public static GameObjectVariableInstances Instance;
    
    {

        get
        {
            return null;
            
            if(_instance == null)
            {
                GameObject newCon = new GameObject("GameObjectVariableInstances");
                DontDestroyOnLoad(newCon);
                GameObjectVariableInstances newConIns = newCon.AddComponent<GameObjectVariableInstances>();
                _instance = newConIns; //new GameObject().AddComponent<GameConditionDataInstance>();
            }

            return _instance;
            
        }
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Multiple instances of GameObjectVariableInstances. Destroying this instance");
            GameObject.Destroy(gameObject);
        }
    }
    public GameObject GameObjectByID(string id)
    {
        return gameObjectVariableInstanceCollection.ContainsKey(id) ? gameObjectVariableInstanceCollection[id] : null;
    }

    public void AddGameObjectByID(string id,GameObject gameObjectValue)
    {
        if (gameObjectVariableInstanceCollection.ContainsKey(id))
        {
            gameObjectVariableInstanceCollection[id] = gameObjectValue;
        }
        else
        {
            gameObjectVariableInstanceCollection.Add(id, gameObjectValue);
        }
    }

    public Dictionary<string, GameObject> gameObjectVariableInstanceCollection = new Dictionary<string, GameObject>();

}
*/