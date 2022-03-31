using System;
using Sirenix.OdinInspector;

namespace DMS.Scriptables
{
    [Serializable,HideLabel]
    public class FloatReference
    {
        public bool UseConstant = true;
    
        [ShowIf("UseConstant")] public float ConstantValue;
        [HideIf("UseConstant")] public FloatVariable Variable;

        public float Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }
    }
}
