using Sirenix.OdinInspector;
using UnityEngine;

namespace DMS.Scriptables
{
    [CreateAssetMenu(menuName = "MattSanders/Variables/FloatVariable")]
    public class FloatVariable : GlobalScriptableWithID
    {
        public override bool AllowDisabling()
        {
            return false;
        }

        private float _value;

        private string ShowName()
        {
            return name;
        }

        [ShowInInspector,DisableInEditorMode]
        public float Value
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

        public System.Action<float> onValueChanged;

        [DisableInPlayMode]
        [BoxGroup("1", ShowLabel = false, Order = 1)]
        [HideInInlineEditors]
        public float startingValue;

        public override void SoSetStartingValue()
        {
            base.SoSetStartingValue();
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
                ES3.Save<float>("Float_" + InternalID, _value, SaveLoad.instance.fileName);
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
                if (ES3.KeyExists("Float_" + InternalID, SaveLoad.instance.fileName))
                {
                    _value = ES3.Load<float>("Float_" + InternalID, SaveLoad.instance.fileName);
                    onValueChanged?.Invoke(_value);
                }
            }
        }
    }
}
