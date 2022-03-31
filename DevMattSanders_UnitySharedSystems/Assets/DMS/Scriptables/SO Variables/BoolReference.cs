using System;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
#endif
namespace DMS.Scriptables
{
    [Serializable]
    public class BoolReference
    {
        [HorizontalGroup("H")]
        [HideInInspector]
        public bool UseConstant = true;

        [HorizontalGroup("H")]
        [InlineButton("Toggle", label: "Use Variable")]
        [ShowIf("UseConstant")] public bool ConstantValue;
        [HorizontalGroup("H")]
        [InlineButton("Toggle",label: "Use Constant")]
        [HideLabel, HideIf("UseConstant")] public BoolVariable Variable;


        private void Toggle()
        {
            if(UseConstant == true)
            {
                UseConstant = false;
            }
            else
            {
                UseConstant = true;
            }
        }

        public bool Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

  
    }
}
/*
#if UNITY_EDITOR
public class BoolReferenceDrawer : OdinValueDrawer<BoolReference>
{
    private InspectorProperty UseConstant;
    private InspectorProperty ConstantValue;
    private InspectorProperty Variable;

    protected override void Initialize()
    {
        base.Initialize();
        UseConstant = this.Property.Children["UseConstant"];
        ConstantValue = this.Property.Children["ConstantValue"];
        Variable = this.Property.Children["Variable"];
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        Rect rect = EditorGUILayout.GetControlRect();

        if(label != null)
        {
            rect = EditorGUI.PrefixLabel(rect, label);
        }

        rect = EditorGUILayout.GetControlRect();

        GUIHelper.PushLabelWidth(20);

        //UseConstant.ValueEntry.WeakSmartValue = SirenixEditorFields.Dropdown<bool>(new GUIContent("d"), UseConstant.ValueEntry);
       
    }
}
#endif
*/
