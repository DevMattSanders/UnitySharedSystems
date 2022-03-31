﻿using UnityEngine;

namespace DevMattSanders._CoreSystems.Build_Version_Number
{
	public class VersionNumberToText : MonoBehaviour
	{
	
		public TMPro.TextMeshProUGUI textMesh;
	
		// Awake is called when the script instance is being loaded.
		protected void Awake()
		{
			textMesh.SetText("v " + Application.version);
		}
	
	}
}