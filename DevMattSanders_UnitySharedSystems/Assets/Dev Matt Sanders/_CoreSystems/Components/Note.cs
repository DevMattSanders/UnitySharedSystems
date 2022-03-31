#if UNITY_EDITOR

using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    public class Note : MonoBehaviour
    {
        [TextArea(10,50)]
        public string note;
    }
}

#endif
