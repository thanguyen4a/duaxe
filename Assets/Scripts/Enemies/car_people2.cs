using UnityEngine;
using System.Collections;

public class car_people2 : ObservableMonoBehaviour {

	private bool isMoveDown = true;
	private bool isMoveUp = false;
	private bool isMoveCriticalUp = false;
	private bool isMoveHorizontal = true;
	public  bool isSingle ;
	public GameObject MainCar;
	void Start () {
		base.OnStart();
		MainCar = GameObject.FindGameObjectWithTag("MainCar");
		randomPosition();
	}
	
	// Update is called once per frame
	void Update () {
		if(MainCar == null ) {MainCar = GameObject.FindGameObjectWithTag("MainCar");return ;}
		//while(MainCar == null)
		//{
		//	MainCar = GameObject.Find("MainCar");
		//}

		if(isMoveDown)
		{
		

			onMoveDown ();
			if(isMoveHorizontal)
			onMoveHorizontal();
		    
		}

		if(isMoveUp)
		{ 
			onMoveUp ();
		}
		if(isMoveCriticalUp)
		{

			onMoveCriticalUp ();
		}
		checkDownDestroy();

	}
	private void onMoveDown()
	{
		Vector3 position = this.gameObject.transform.position;
		position.y -= Constant.enemy_delta*Time.deltaTime*1.5f;
		this.gameObject.transform.position = position;
	}
	
	private void onMoveCriticalUp()
	{
		Vector3 position = this.gameObject.transform.position;
		checkUpDestroy();
		position.y += Constant.enemy_critical_updelta*Time.deltaTime;
		this.gameObject.transform.position = position;
	}
	
	private void onMoveUp()
	{
		Vector3 position = this.gameObject.transform.position;
		checkUpDestroy();
		position.y += Constant.enemy_updelta*Time.deltaTime;
		this.gameObject.transform.position = position;
	}
	
	private void checkDownDestroy()
	{		if(this.gameObject == null )return;
		Vector3 position = this.gameObject.transform.position;
		
		if( position.y < -Constant.road_lenght) 
			GameObject.Destroy(this.gameObject);		
	}
	
	private void checkUpDestroy()
	{		
		if(this.gameObject == null )return;
		Vector3 position = this.gameObject.transform.position;
		
		if( position.y > Constant.road_lenght) 
			GameObject.Destroy(this.gameObject);		
	}
	

	
	private void onMoveHorizontal()

	{   if(MainCar == null )return ;

		Vector3 mainCarPosition = MainCar.transform.position;
		if(transform.position.y - mainCarPosition.y < Constant.enemy_notfollow_length)
			return ;
		//Debug.Log (" main :  " +  mainCarPosition.x + "  Enemy: " + transform.position.x);
		if(transform.position.x > mainCarPosition.x)
		{
			Vector3 position = this.gameObject.transform.position;
			position.x -= Constant.enemy_Horizontal_delta*Time.deltaTime;
			this.gameObject.transform.position = position;
		}

		if(transform.position.x < mainCarPosition.x)
		{
			Vector3 position = this.gameObject.transform.position;
			position.x += Constant.enemy_Horizontal_delta*Time.deltaTime;
			this.gameObject.transform.position = position;
		}
	}

	private void randomPosition()
	{
		if(isSingle)
		{
			Vector3 position = this.gameObject.transform.position;
			position.x =Random.Range(-Constant.position_random_range_car_people2,+Constant.position_random_range_car_people2);
			this.gameObject.transform.position = position;
		}
	}


	void OnTriggerEnter2D(Collider2D others)
	{
		if(others.tag == "Road")
		{   
			isMoveHorizontal = false;
		}
		if(others.tag == "main_pivot")
		{   
			this.sendMessage("The car have passed enemy" ,1);
			//Debug.LogWarning("The car have passed enemy 2");
			
		}
	}

	void OnTriggerStay2D (Collider2D others)
	{

	}
	
	
	public override void updateMessage (string message, object data, ObservableObject sender)
	{
		if(message == "PLAYER_ONDIE")
		{
			isMoveDown = false;
			isMoveUp = false;
			isMoveCriticalUp = true;
		}
		if(message == "PLAYER_ONROTATE")
		{
			isMoveDown = false;
			isMoveUp = true;
			isMoveCriticalUp = false;
		}
	}
}
