using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    [CreateAssetMenu(menuName = "MattSanders/Variables/GameObjectVariable")]

    public class GameObjectVariable : GlobalScriptableWithID
    {
        public string testValueGO = "";
        //public static List<game>

        // public static Dictionary<string, GameObject> instances = new Dictionary<string, GameObject>();

        public override bool AllowDisabling()
        {
            return false;
        }
    
        // [SerializeField]
        // private string gameObjectID;
        [SerializeField]
        private GameObject _value;
        // private string instanceID;
        private string ShowName() { return name; }

        // [LabelText("$ShowName")]
        // [ShowInInspector]

        public int goCounter = 0;

        public void AddRef(GameObject go)
        {
            //Debug.Log("Add Ref: " + goCounter + " " + go.name); 
            if (goCounter > 0) return;
            goCounter++;
            _value = go;
        }

        public void RemoveRef(GameObject go)
        {
            if(goCounter < 1)
            {
                return;
            }
            //   Debug.Log("Remove Ref: " + goCounter + " " + go.name);
            goCounter--;
            _value = null;
        }

        public GameObject Value
        {
            get
            {
                Debug.Log("ID: " + _value);
                // if (string.IsNullOrEmpty(gameObjectID))
                if(_value == null)
                {
                    //  Debug.Log("Missing My _value!");
                    return null;
                }
                else
                {
                    //  Debug.Log("Returning _value");
                    return _value;
                    //return GameObjectVariableInstances.Instance.GameObjectByID(gameObjectID);
                }
            }
            /*
        set
        {
            Debug.Log("_Setting New _value");
            //  gameObjectID = value.GetInstanceID().ToString();
            //   GameObjectVariableInstances.Instance.AddGameObjectByID(gameObjectID,value);
            _value = value;
            onValueChanged?.Invoke(_value);
           
        }
        */
        }

        public System.Action<GameObject> onValueChanged;

        /*
    [DisableInPlayMode]
    [BoxGroup("1", ShowLabel = false, Order = 1)]
    [HideInInlineEditors]
    public GameObject startingValue;
     */

 
   

        public override void SoSetStartingValue()
        {
            base.SoSetStartingValue();
            //  Debug.Log("Setting _value to Null from START");
            testValueGO = "";
            _value = null;// null;
            goCounter = 0;
        }

        public override void SoEnd()
        {
            base.SoEnd();
            //  Debug.Log("Setting _value to Null from END");
            testValueGO = "";
            _value = null;
            goCounter = 0;
        }

        /*
    public override void Save()
    {
        if (allowSaveAndLoad == false)
        {
            return;
        }

        if (Application.isPlaying == false) return;
        base.Save();


        
        if (SaveLoad.instance != null)
        {
            ES3.Save<GameObject>("GameObject_" + InternalID, _value, SaveLoad.instance.fileName);
        }

    }

    public override void Load()
    {
        if (allowSaveAndLoad == false)
        {
            return;
        }
        if (Application.isPlaying == false) return;
        base.Load();
        if (SaveLoad.instance != null)
        {
            if (ES3.KeyExists("GameObject_" + InternalID, SaveLoad.instance.fileName))
            {
                _value = ES3.Load<GameObject>("GameObject_" + InternalID, SaveLoad.instance.fileName);
                onValueChanged?.Invoke(_value);
            }
        }
    }
    */
    }
}
