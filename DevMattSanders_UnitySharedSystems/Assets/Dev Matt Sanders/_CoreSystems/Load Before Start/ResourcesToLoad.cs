using UnityEngine;

namespace DevMattSanders._CoreSystems
{
	public class ResourcesToLoad : MonoBehaviour
	{
		// [SerializeField] private List<GameObject> gameObjectsToLoad = new List<GameObject>();
		public LoadMainBeforeScene loadMainBeforeScene;
		public void LoadResources()
		{
			foreach(GameObject next in loadMainBeforeScene.gameObjectsToCreateAtStart)
			{
				//Debug.Log("LOADING RESOURCES!");
				GameObject go = GameObject.Instantiate(next);

				GameObject.DontDestroyOnLoad(go);
       
				go.SendMessage("OnCreatedBeforeScene",SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
