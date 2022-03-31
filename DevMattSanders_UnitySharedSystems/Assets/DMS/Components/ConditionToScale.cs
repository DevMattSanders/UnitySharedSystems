using System.Collections.Generic;
using DMS.Scriptables;
using UnityEngine;

namespace DMS.Components
{
    public class ConditionToScale : MonoBehaviour
    {
        public List<ConditionToScaleCase> scaleCases = new List<ConditionToScaleCase>();
    }

    [System.Serializable]
    public class ConditionToScaleCase
    {
        public GameCondition condition;
        public Vector3 scale;
    }
}