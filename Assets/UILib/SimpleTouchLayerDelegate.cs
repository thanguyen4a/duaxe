using UnityEngine;
using System;

namespace UILib
{
	public interface SimpleTouchLayerDelegate
	{
		void onTouchBegin(UnityEngine.Vector2 pos);
		void onTouchEnd(UnityEngine.Vector2 pos);
	}
}

