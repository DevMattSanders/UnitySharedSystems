#if UNITY_EDITOR

using UnityEngine;

namespace DMS.Components
{
    public class Note : MonoBehaviour
    {
        [TextArea(10,50)]
        public string note;
    }
}

#endif
