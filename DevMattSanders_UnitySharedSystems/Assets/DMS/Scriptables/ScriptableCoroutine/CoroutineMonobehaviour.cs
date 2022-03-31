using UnityEngine;

namespace DMS.Scriptables
{
	public class CoroutineMonobehaviour : MonoBehaviour
	{
		public static CoroutineMonobehaviour instance;
	
		// Awake is called when the script instance is being loaded.
		protected void Awake()
		{
			if(instance != null){
				GameObject.Destroy(gameObject);
			}else{
				instance = this;
			}
		}
	
	
	}
}
