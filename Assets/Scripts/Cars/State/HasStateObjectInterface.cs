using UnityEngine;
using System.Collections;

public interface HasStateObjectInterface
	{
		void setState(ICar new_state);
		void firedEvent(string event_name);
	}

