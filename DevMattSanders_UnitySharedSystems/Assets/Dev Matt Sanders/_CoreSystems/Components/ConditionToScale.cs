using System.Collections.Generic;
using UnityEngine;

namespace DevMattSanders._CoreSystems
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