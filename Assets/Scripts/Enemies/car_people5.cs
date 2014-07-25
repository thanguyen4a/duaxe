using UnityEngine;
using System.Collections;

public class car_people5 : ObservableMonoBehaviour {
	enum MoveDirect{
		Left,Right
	}
	private MoveDirect move_direct;
	private bool isMoveDown = true;
	private bool isMoveUp = false;
	private bool isMoveCriticalUp = false;
	private bool EnablechangeMoving = true;
	private bool isMoveHorizontal = true;
	private int roadCollision = 0;
	public  bool isSingle ;
	// Use this for initialization
	void Start () {
		base.OnStart();
		randomPosition();
		float _random = Random.Range(0,1);
		if (_random < 0.5f)
			move_direct = MoveDirect.Left;
		else
		    move_direct = MoveDirect.Right;



	}
	// Update is called once per frame
	void Update () {
		if(isMoveDown)
			onMoveDown ();
		if(isMoveUp)
			onMoveUp ();
		if(isMoveCriticalUp)
			onMoveCriticalUp ();
		checkDownDestroy();
	}
	
	private void onMoveDown()
	{
		Vector3 position = this.gameObject.transform.position;
		position.y -= Constant.enemy_delta*Time.deltaTime*2f;
		if(isMoveHorizontal)
		position.x += getHorizontalMoving()*Constant.enemy_Horizontal_delta*Time.deltaTime*3.0f;
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

	private IEnumerator changeMoving()
	{
		EnablechangeMoving = false;
		if(this.move_direct == MoveDirect.Left)
		this.move_direct = MoveDirect.Right;
		else		
		this.move_direct = MoveDirect.Left;	
		yield return new WaitForSeconds (0.05f);
		roadCollision = 1;
	}

	private int getHorizontalMoving()
	{
		//Debug.Log("Huong " +move_direct );
		if(this.move_direct == MoveDirect.Left)
			return -1;

		return 1;
	}

	private void randomPosition()
	{
		if(isSingle)
		{
			Vector3 position = this.gameObject.transform.position;
			position.x =Random.Range(-Constant.position_random_range_car_people5,+Constant.position_random_range_car_people5);
			this.gameObject.transform.position = position;
		}
	}

	void OnTriggerEnter2D(Collider2D others)
	{
		if(others.tag == "Road")
		{   
			if(this.EnablechangeMoving == true)
				StartCoroutine(changeMoving());

			if(roadCollision == 1)
				isMoveHorizontal = false;
		}
		if(others.tag == "main_pivot")
		{   
			this.sendMessage("The car have passed enemy" ,1);
			//Debug.LogWarning("The car have passed enemy 5");
			
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
