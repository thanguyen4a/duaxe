using UnityEngine;
using System.Collections;

public class RestartButton :ObservableMonoBehaviour,UILib.SimpleButtonDelegate{

	// Use this for initialization
	void Start () {
		base.OnStart();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClicked(UnityEngine.GameObject gameobject)
	{
		//Debug.Log ("dc click");

	}

	public void onClickEnd(UnityEngine.GameObject gameobject)
	{
		//Debug.Log ("dc click end");
		sendMessage("RESET_GAME" , null); 
		GameObject.Destroy(this.gameObject);
	}

}
