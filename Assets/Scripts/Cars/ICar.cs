using UnityEngine;
using System.Collections;

public interface ICar {
	void switchCarLeftRotateState();
	void switchCarRightRotateState();
	void switchCarIdleState();
	void switchCarAttackState();
	void switchCarDieState();
	void switchFlyState();
}
