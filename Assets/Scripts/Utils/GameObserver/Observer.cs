using System;
using System.Collections;

public interface Observer{
	void notify(string message, Object data, ObservableObject sender);
	string addObservableObject(ObservableObject observer);
	void removeObservableObject(string observer_id);
}
