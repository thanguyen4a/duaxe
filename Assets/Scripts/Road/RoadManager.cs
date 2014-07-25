using UnityEngine;
using System.Collections;

public class RoadManager : ObservableMonoBehaviour {

	// Use this for initialization
	public GameObject MainCar;
	public bool MainRoad;
	private float prePosition;
	private GameState isPlay = GameState.Playing;
	void Start () {
		base.OnStart();
		//MainCar = GameObject.Find("MainCar(Clone)");
		MainCar = GameObject.FindGameObjectWithTag("MainCar");
		prePosition = transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {

		if(MainCar == null) {MainCar = GameObject.FindGameObjectWithTag("MainCar");return ;}

		if(isPlay != GameState.Stop )
		{
			moveUpdate();
		}
		jumpUpdate();
	}


	private void moveUpdate()
	{
		checkIsSpawnCar();
		Vector3 position = this.gameObject.transform.position;
		float speed_lose = 1f;
		if(isPlay == GameState.PrepareStop)speed_lose = 0.5f;
		position.y -= speed_lose*Constant.road_delta*Time.deltaTime;
		this.gameObject.transform.position = position;
	}

	private void jumpUpdate()
	{	
		if(MainCar.transform.position.y - this.gameObject.transform.position.y >= 0.92*Constant.road_lenght)
		{
			Vector3 position = this.gameObject.transform.position;
			position.y += 3*Constant.road_lenght;
			prePosition += 3*Constant.road_lenght;
			this.gameObject.transform.position = position;
		}
	}

	private void checkIsSpawnCar()
	{
		if(prePosition - this.transform.position.y >= Constant.random_distance && MainRoad)
		{
			sendMessage("SPAWN_ENEMY", null);
			prePosition = transform.position.y;
		}
	}


	public override void updateMessage (string message, object data, ObservableObject sender)
	{
		if(message == "PLAYER_ONDIE")
		{
			isPlay = GameState.Stop;
		}
		if(message == "PLAYER_ONROTATE")
		{
			if(isPlay == GameState.Playing)
			isPlay = GameState.PrepareStop;
			Debug.Log ("Bi daaaaaaaaaaaaaaaaaaaaaaaaaaaa");
		}

		if(message == "RESET_GAME")
		{
			isPlay = GameState.Playing;
		}
	}
}
