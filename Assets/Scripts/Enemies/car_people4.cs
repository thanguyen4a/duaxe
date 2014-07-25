using UnityEngine;
using System.Collections;

public class car_people4 : ObservableMonoBehaviour {
	
	// Use this for initialization
	private bool isMoveDown = true;
	private bool isMoveUp = false;
	private bool isMoveCriticalUp = false;
	private bool isMoveHorizontal = true;
	public  bool isSingle ;



	private float gravityY = 12f;  
	private float originalspeedY = 0f;
	private float speedY = 0f;
	private float cycle = 1f;
	private float positive_percent = 2/3f;
	private float originalX ;





	void Start () {
		base.OnStart();
		randomPosition();
		originalX = transform.position.x ;
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
		if(isMoveHorizontal)
		{
			position.x += Constant.enemy_Horizontal_delta*Time.deltaTime;
			float delta =position.x - originalX;
			float remain  = delta - ((int)(delta/cycle)* cycle );
			if(remain >= 0 && remain <  cycle*positive_percent )
			{
				if( originalspeedY < 0)
					originalspeedY = 0;

				originalspeedY +=  gravityY*Time.deltaTime;
				position.y -= originalspeedY*Time.deltaTime;
			}
			else
			{
				if( originalspeedY > 0)
					originalspeedY *= -1;
				originalspeedY +=  gravityY*Time.deltaTime;
				position.y -= originalspeedY*Time.deltaTime;
			}

		}

		position.y -= Constant.enemy_delta*Time.deltaTime;
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

	private void randomPosition()
	{
		if(isSingle)
		{
			Vector3 position = this.gameObject.transform.position;
			position.x =Random.Range(-Constant.position_random_range_car_people4,+Constant.position_random_range_car_people4);
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
			//Debug.LogWarning("The car have passed enemy 4");
			
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
