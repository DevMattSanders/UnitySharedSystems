using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR


#endif
namespace DMS.Scriptables
{
    public class GlobalScriptableWithID : GlobalScriptable, ISerializationCallbackReceiver
    {
        public static readonly Dictionary<GlobalScriptableWithID, string> ObjectToString =
            new Dictionary<GlobalScriptableWithID, string>();

        public static readonly Dictionary<string, GlobalScriptableWithID> StringToObject =
            new Dictionary<string, GlobalScriptableWithID>();

 

#if UnityEditor
	[HideInInlineEditors]
#endif
        [HideInInlineEditors]
        public bool allowSaveAndLoad = true;

   
        [SerializeField]
#if UNITY_EDITOR 
    [VerticalGroup("V")]
   // [InlineButton("RefreshID")]
    [ReadOnly]
    [HideInInlineEditors]
#endif
        private string internalId;

        [NonSerialized]
        private bool _internalIdWasUpdated;

  
        public string InternalID => internalId;

        //  [ShowIf("ShowSetID")]
        [VerticalGroup("V")]
        [Button]
        [HideInInlineEditors]
        public void RefreshID()
        {
            ProcessRegistration(this);
        }
        /*
    private bool ShowSetID()
    {
        if (string.IsNullOrEmpty(internalId))
        {
            return true;
        }
        return false;
    }
    */
        public override void SoSetStartingValue()
        {
            base.SoStart();

            ProcessRegistration(this);

            if (!_internalIdWasUpdated)
            {
                return;
            }

            _internalIdWasUpdated = false;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
        }

        protected void OnDestroy()
        {
            Debug.Log($"Unexpected object destroyed. {internalId}");
            UnregisterObject(this);
            internalId = null;
        }

        public void OnAfterDeserialize()
        {
            //    ProcessRegistration(this);
        }

        public void OnBeforeSerialize()
        {
            //     ProcessRegistration(this);
        }

        private static void ProcessRegistration(GlobalScriptableWithID obj)
        {
            if (ObjectToString.TryGetValue(obj, out var existingId))
            {
                if (obj.internalId != existingId)
                {
                    Debug.LogError($"Inconsistency: {obj.name} {obj.internalId} / {existingId}");
                    obj.internalId = existingId;
                }

                if (StringToObject.ContainsKey(existingId))
                {
                    return;
                }

                Debug.Log("Inconsistent database tracking.");
                StringToObject.Add(existingId, obj);

                return;
            }

            if (string.IsNullOrEmpty(obj.internalId))
            {
                GenerateInternalId(obj);

                RegisterObject(obj);
                return;
            }

            if (!StringToObject.TryGetValue(obj.internalId, out var knownObject))
            {
                RegisterObject(obj);
                return;
            }

            if (knownObject == obj)
            {
                Debug.Log("Inconsistent database tracking.");
                ObjectToString.Add(obj, obj.internalId);
                return;
            }

            if (knownObject == null)
            {
                Debug.Log("Unexpected registration problem.");
                RegisterObject(obj, true);
                return;
            }

            GenerateInternalId(obj);

            RegisterObject(obj);
        }

        private static void RegisterObject(GlobalScriptableWithID aID, bool replace = false)
        {
            if (replace)
            {
                StringToObject[aID.internalId] = aID;
            }
            else
            {
                StringToObject.Add(aID.internalId, aID);
            }

            ObjectToString.Add(aID, aID.internalId);
        }

        private static void UnregisterObject(GlobalScriptableWithID aID)
        {
            StringToObject.Remove(aID.internalId);
            ObjectToString.Remove(aID);
        }

        private static void GenerateInternalId(GlobalScriptableWithID obj)
        {
            obj.internalId = Guid.NewGuid().ToString();
            obj._internalIdWasUpdated = true;
        }
    }
}
