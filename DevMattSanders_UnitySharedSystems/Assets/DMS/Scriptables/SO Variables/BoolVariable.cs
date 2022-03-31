using Sirenix.OdinInspector;
using UnityEngine;

namespace DMS.Scriptables
{
    [CreateAssetMenu(menuName = "MattSanders/Variables/BoolVariable")]
    public class BoolVariable : GlobalScriptableWithID
    {
        public string testValueBool = "";
        public override bool AllowDisabling()
        {
            return false;
        }
        private bool _value;


        private string ShowName() { return name; }


        [LabelText("$ShowName")]
        [ShowInInspector]
        public bool Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                onValueChanged?.Invoke(_value);
                Save();

            }


        }
        // [HideInInspector]
        // public UnityEvent<bool> onValueChanged = new UnityEvent<bool>();

        public System.Action<bool> onValueChanged;

        [DisableInPlayMode]
        [BoxGroup("1", ShowLabel = false, Order = 1)]
        [HideInInlineEditors]
        public bool startingValue;

        public override void SoSetStartingValue()
        {
            base.SoSetStartingValue();
            testValueBool = "";
        }

        public override void SoStart()
        {
            base.SoStart();
            _value = startingValue;
        }


        public override void SoEnd()
        {
            base.SoEnd();
            _value = startingValue;
        }

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
                ES3.Save<bool>("Bool_" + InternalID, _value, SaveLoad.instance.fileName);
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
                if (ES3.KeyExists("Bool_" + InternalID, SaveLoad.instance.fileName))
                {
                    _value = ES3.Load<bool>("Bool_" + InternalID, SaveLoad.instance.fileName);
                    onValueChanged?.Invoke(_value);
                }
            }
        }
    }
}


