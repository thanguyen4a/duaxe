using UnityEngine;
using System.Collections;

public interface ICarState {
	 void enter(ICar car);
	 void handleInput(ICar car,string input);
	 void update(ICar car ,string message);
}
