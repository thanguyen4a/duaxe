using UnityEngine;
using System.Collections;
using System.Runtime;

class GameObserverData{
	public string observer_id;
	public ObservableObject observer_ob;
	private static int seed = 0;
	public GameObserverData(ObservableObject _observer_ob){	
		//observer_id = SpeedX.Utils.StringUtils.RandomString(15)+randomTime();	
		seed++;
		observer_id = "" + seed;
		observer_ob = _observer_ob;

	}
	/*
	private string randomTime()
	{
		return (RealTime.time).ToString();
	}*/
}



public class GameObserver : Observer {
	ArrayList objects;
	private static Observer singleton;
	
	public GameObserver(){
		//Debug.Log("Make GameObserver");
		objects = new ArrayList();	
	}
	
	public static Observer getSingleton(){
		if(singleton == null){
			singleton = new GameObserver();
		}
		return singleton;
	}
	
	/*****************************/
	/*	Observer Implementation	 */
	/*****************************/
	public void notify(string message, System.Object data, ObservableObject sender){
		for(int i =0; i< objects.Count; i++){
			GameObserverData observer_data = (GameObserverData)objects[i];
			observer_data.observer_ob.updateMessage(message, data, sender);
		}
	}
	
	public string addObservableObject(ObservableObject observer){
		GameObserverData g_observer_data = new GameObserverData(observer);
		objects.Add(g_observer_data);
		//Debug.Log("Observer added:" + g_observer_data.observer_id);

		for(int i =0; i< objects.Count; i++){
			GameObserverData observer_data = (GameObserverData)objects[i];
			//Debug.Log ("\t\tHas:" + observer_data.observer_id);
		}

		return g_observer_data.observer_id;
	}
	
	public void removeObservableObject(string observer_id){
		bool checker = false;

		//Debug.Log("Remove observer object:"+observer_id);
		for(int i =0; i< objects.Count; i++){
			GameObserverData observer_data = (GameObserverData)objects[i];
			//Debug.Log ("Check observer " + observer_data.observer_id + " -- " + observer_id);


			if(observer_data != null && observer_id != null)
			{
			if( observer_id.CompareTo(observer_data.observer_id) == 0){
				//Debug.Log ("\t\tremoved observer");
				checker = true;

				objects.RemoveAt(i);
			}
			}
		}

		if(!checker){
			for(int i =0; i< objects.Count; i++){
				GameObserverData observer_data = (GameObserverData)objects[i];
				//Debug.Log ("\t\tHas:" + observer_data.observer_id);
			}
		}
	}
}
