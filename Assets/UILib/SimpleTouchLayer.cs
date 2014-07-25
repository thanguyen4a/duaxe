using UnityEngine;
using System.Collections;

namespace UILib{
	public class SimpleTouchLayer : MonoBehaviour {
		public GameObject delegate_object;
		
		private SimpleTouchLayerDelegate touch_delegate;
		// Use this for initialization
		void Start () {
			Debug.Log("[SimpleTouchLayer] init");
			touch_delegate = (SimpleTouchLayerDelegate)delegate_object.GetComponent(typeof(SimpleTouchLayerDelegate));
		}
		
		// Update is called once per frame
		void Update () {
			if(Input.GetMouseButtonDown(0)) {
				Vector3 touch = Input.mousePosition;
				Vector2 touched_pos = new Vector2(touch.x, touch.y);
				touch_delegate.onTouchBegin(touched_pos);	
			}else if(Input.GetMouseButtonUp(0)) {
				Vector3 touch = Input.mousePosition;
				Vector2 touched_pos = new Vector2(touch.x, touch.y);
				touch_delegate.onTouchEnd(touched_pos);
			}
			
		}
	}
}
