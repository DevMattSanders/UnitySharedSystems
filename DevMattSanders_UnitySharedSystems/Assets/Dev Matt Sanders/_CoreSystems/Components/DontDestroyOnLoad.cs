using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        /// <summary>
        /// On Awake we make sure our object will not destroy on the next scene load
        /// </summary>
        protected void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
