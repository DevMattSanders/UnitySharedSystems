using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DevMattSanders._CoreSystems
{
	[CreateAssetMenu(menuName = "MattSanders/ProjectEssential/LoadMainBeforeScene")]
	public class LoadMainBeforeScene : ScriptableObject
	{
		[HideReferenceObjectPicker]
		[ListDrawerSettings(Expanded = true, HideRemoveButton = true)]
		public List<GameObject> gameObjectsToCreateAtStart = new List<GameObject>();
    
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void LoadMain()
		{
			Debug.Log("------- Loading Before Scene -------");
			GameObject createdObject = GameObject.Instantiate(Resources.Load("Main")) as GameObject;
			GameObject.DontDestroyOnLoad(createdObject);

			createdObject.GetComponent<ResourcesToLoad>().LoadResources();
		}
	}
}
