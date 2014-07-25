using System;
using System.Collections;

public interface ObservableObject{
	void updateMessage(string message, Object data, ObservableObject sender);
	void sendMessage(string message, Object data);
}
