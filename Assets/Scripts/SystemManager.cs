using UnityEngine;
using System.Collections;

public class SystemManager : ObservableMonoBehaviour{

	// Use this for initialization
	private bool acttiveEnergy = true;

	public int numberOfCars = 0;
	public int numberOfGroups = 0 ;
	public GameObject enemySpawn ,Items ,numberOfRespawnAvatar;
	public GameObject MainCar;
	public Texture energy ,remain_energy;
	public float remainEnergy;


	void Start () {
		base.OnStart(); 	 
		MainCar = loadPrefab(Constant.maincar_prefab_direct + "MainCar");
		enemySpawn = GameObject.Find("enemySpawn");
		Items = GameObject.Find("Items");
		numberOfRespawnAvatar = GameObject.Find("respawnNumber");
		remainEnergy = Constant.car_max_energy;
	}
	
	// Update is called once per frame
	void Update () {	
		if(acttiveEnergy)
		{
			remainEnergy -=  Constant.lost_energy_percent*Time.deltaTime ;
			//Debug.Log ("nang luong bi mat sau moi frame " + remainEnergy);
		}

	}

	private GameObject loadPrefab(string directory)
	{
		return Instantiate(Resources.Load(directory)) as GameObject;
	}

	private GameObject loadEnemyPrefab(string enemy_name)
	{
		GameObject obj = Instantiate(Resources.Load(Constant.enemy_prefab_direct + enemy_name)) as GameObject;
		obj.transform.parent = enemySpawn.transform;
		return obj;
	}

	private GameObject loadItemPrefab(string item_name)
	{
		GameObject obj = Instantiate(Resources.Load(Constant.item_direct + item_name)) as GameObject;
		obj.transform.parent = Items.transform;
		return obj;
	}

	private  GameObject SpawnCar()
	{
		numberOfGroups++;
		if (numberOfGroups > 0 && numberOfGroups %3 == 0)
		{
			this.loadItemPrefab("gasoline");
		}
	     
		if(numberOfGroups <=5)	
			return loadEnemyPrefab("car_people" + Random.Range(1,8));

		if(numberOfGroups >5)
		{
			loadEnemyPrefab("car_people" + Random.Range(1,8));
			return loadEnemyPrefab("car_people" + Random.Range(1,8));
		}

		return null;

	}

	private void DestroyAllEnemies()
	{	
		foreach(Transform children in enemySpawn.transform)
		{
			Destroy(children.gameObject);
		}
	}

	private void DestroyAllItems()
	{	
		foreach(Transform children in Items.transform)
		{
			Destroy(children.gameObject);
		}
	}


	private void resetData() 
	{
		numberOfGroups = 0;
		numberOfCars = 0;
	}

	void OnGUI()
	{    GUI.DrawTexture(new Rect(10,10,140,10),energy);
		if(remainEnergy > 0 && remainEnergy <= Constant.car_max_energy) 
	    GUI.DrawTexture (new Rect(10,10,(int)(140*remainEnergy/Constant.car_max_energy*1.0),10),remain_energy);
		if(remainEnergy <=0 && acttiveEnergy)
		{
			sendMessage("ENERGY_OUT",null);
			acttiveEnergy = false;

		}
	}


	public override void updateMessage (string message, object data, ObservableObject sender)
	{
		if(message == "The car have passed enemy")
		{
			numberOfCars++;
			//Debug.LogWarning("numberOfCars = " + numberOfCars);
			//Debug.Log("numberOfCars = ");
		}

		if(message == "SPAWN_ENEMY")
		{
			SpawnCar();
		}

		
		if(message == "PLAYER_ONDIE")
		{
			loadPrefab(Constant.button_direct+"restartButton");
			Debug.Log ("Dieeeeeeeeeeeeeeee");
			acttiveEnergy  = false;
		}

		if(message == "RESET_GAME")
		{

			DestroyAllEnemies();
			DestroyAllItems();
			GameObject.Destroy(MainCar);
			MainCar = loadPrefab(Constant.maincar_prefab_direct + "MainCar");
			remainEnergy = Constant.car_max_energy;
			acttiveEnergy  = true;
		}


		if(message == "TAKE_ENERGY")
		{
			remainEnergy += Constant.car_max_energy*Constant.percentofGasoline;
			if(remainEnergy>Constant.car_max_energy)
				remainEnergy = Constant.car_max_energy;
		}

	}
}
