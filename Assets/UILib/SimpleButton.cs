using UnityEngine;
using System.Collections;

namespace UILib{
	public class SimpleButton : MonoBehaviour{
		public GameObject delegate_object;
		
		public bool enable;
		
		public Sprite defaultImage;
		public Sprite clickedImage;
		public Sprite disableImage;

		private SpriteRenderer renderer;
		private SimpleButtonDelegate button_delegate;

		// Use this for initialization
		void Start () {
			renderer = this.GetComponent<SpriteRenderer>();
			renderer.sprite = defaultImage;
			//Debug.Log("delegate"+delegate_object);
			button_delegate = (SimpleButtonDelegate)(delegate_object.GetComponent(typeof(SimpleButtonDelegate)));

			this.SetEnable(this.enable);
		}

		// Update is called once per frame
		void Update () {


		}
		
		//detect touch or click to this object
		void OnMouseDown(){
			if(!enable) return;
		//this.SetSprite(clickedImageIndex);

			renderer = this.GetComponent<SpriteRenderer>();
			{renderer.sprite = clickedImage;
				//Debug.Log("In OnMouseDown SimpleButton");
			}
			if(button_delegate != null) button_delegate.onClicked(gameObject);

		}
			
		//detect touch or click end
		void OnMouseUp(){
			if(!enable) return;
			//this.SetSprite(defaultImageIndex);
			renderer = this.GetComponent<SpriteRenderer>();
		   renderer.sprite = defaultImage;
			if(button_delegate != null) button_delegate.onClickEnd(gameObject);
		}
		
		//private void SetSprite(int index){
		//	if(index >= 0) button_sprite.SetSprite(button_sprite.atlas, index, false);
	//	}
		
		/*****************************/
		/*	ButtonInterface			 */
		/*****************************/
		void SetEnable(bool enable){
			this.enable = enable;
			if(enable){
				//this.SetSprite(defaultImageIndex);
				renderer.sprite = defaultImage;
			}else{
				//this.SetSprite(disableImageIndex);
				renderer.sprite = defaultImage;
			}
		}
	}
}
