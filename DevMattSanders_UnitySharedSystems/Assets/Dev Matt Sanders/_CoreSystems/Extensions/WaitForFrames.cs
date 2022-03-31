﻿using UnityEngine;

namespace DevMattSanders._CoreSystems
{
	public class WaitForFrames : CustomYieldInstruction
	{
		private int _targetFrameCount;

		public WaitForFrames(int numberOfFrames)
		{
			_targetFrameCount = Time.frameCount + numberOfFrames;
		}

		public override bool keepWaiting
		{
			get
			{
				return Time.frameCount < _targetFrameCount;
			}
		}
	}
}