using UnityEngine;
using System.Collections;

public class MainController :ObservableMonoBehaviour {
	
	enum MoveDirect
	{Left ,Right}

	enum CarState{
		Moving,Die,Flying,Rotating
	}
	
	private MoveDirect _move_direct;
	private int move_Horizontal = 0;
	private Animator animator;
	private bool can_control = true;
	private CarState carstate = CarState.Moving;
	// Use this for initialization
	void Start () {
		_move_direct = MoveDirect.Left;
		base.OnStart();
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			if(carstate == CarState.Moving)
				this.CarOnFly();
		}
		
		float h = Input.GetAxis("Horizontal")*Constant.main_delta;
		if(h>0)_move_direct = MoveDirect.Right;
		if(h<0)_move_direct = MoveDirect.Left;
		if(can_control)
			this.transform.Translate(Vector2.right * h*Time.deltaTime);
		
		
		if(move_Horizontal!= 0)
			moveHorizontal(move_Horizontal);
	}
	
	private void moveLeft()
	{
		Vector3 position  =  this.transform.position;
		position.x -= Constant.main_delta;
		this.transform.position = position;
		_move_direct = MoveDirect.Left;
	}
	
	private void moveRight()
	{
		Vector3 position  =  this.transform.position;
		position.x += Constant.main_delta;
		this.transform.position = position;
		
	}
	
	void OnTriggerEnter2D(Collider2D others)
	{
		if(others.tag == "Road")
		{ 	if(carstate != CarState.Die)
			{					
					
					CarOndie();
			}
			move_Horizontal = 0;
		}
		
		if(others.tag == "Enemy")
		{
			if(carstate == CarState.Moving || carstate == CarState.Rotating)
				CarOnRotate();
		}
		
		if(others.tag == "gasoline")
		{
			if(carstate == CarState.Moving|| carstate == CarState.Flying)
			{
				GameObject.Destroy(others.gameObject);
				CarTakeEnergy();
			}
			
		}
	}
	
	private void CarOndie()
	{
		carstate = CarState.Die;
		this.sendMessage("PLAYER_ONDIE" ,null);
		animator.SetTrigger("Die");
		can_control = false;
		
		//GameObject.Destroy(gameObject);
	}
	
	private void CarOnRotate()
	{
		if(this._move_direct == MoveDirect.Left)
		{ 
			CarOnRotateRight();
			this._move_direct = MoveDirect.Right;
		}
		else
		{
			CarOnRotateLeft();
			this._move_direct = MoveDirect.Left;
		}
		this.sendMessage("PLAYER_ONROTATE" ,null);
		carstate = CarState.Rotating;
		can_control = false;
	}
	
	private void CarOnRotateLeft()
	{
		this.sendMessage("PLAYER_ONROTATE_LEFT" ,null);
		animator.SetTrigger("RotateLeft");
		move_Horizontal = -1;
	}
	
	private void CarOnRotateRight()
	{
		this.sendMessage("PLAYER_ONROTATE_RIGHT" ,null);
		animator.SetTrigger("RotateRight");
		move_Horizontal = 1;
	}
	
	private void CarOnFly()
	{
		Debug.Log ("BAyyyyyyyyyyyyyyy");
		this.sendMessage("PLAYER_ONFLY" ,null);
		animator.SetTrigger("Fly");
		carstate = CarState.Flying;
		
	}
	
	
	public void CarLand()
	{
		Debug.Log ("Tiep dat thanh cong");
		carstate = CarState.Moving;
	}
	
	private void CarTakeEnergy()
	{
		sendMessage("TAKE_ENERGY",null);
		
	}
	
	
	private void moveHorizontal(int Horizontal)
	{
		Vector3 position = this.gameObject.transform.position;
		position.x += Horizontal*Constant.Horizontal_delta*Time.deltaTime;
		this.gameObject.transform.position = position;
		
	}
	
	
	public override void updateMessage (string message, object data, ObservableObject sender)
	{
		if(message == "ENERGY_OUT")
		{
			CarOndie();
		}
		
	}
	
	
	
	
	
	
}
