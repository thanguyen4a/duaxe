using UnityEngine;
using System;

namespace UILib
{
	public interface SimpleButtonDelegate
	{
		void onClicked(UnityEngine.GameObject go);
		void onClickEnd(UnityEngine.GameObject go);
	}
}

