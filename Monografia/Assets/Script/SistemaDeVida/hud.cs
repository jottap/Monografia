using UnityEngine;
using System.Collections;

public class hud : MonoBehaviour
{

		private float fullWidth;

		void Awake ()
		{
				this.fullWidth = this.GetComponent<RectTransform> ().rect.width;
		}

		public void SetWidth (float amount)
		{
				this.GetComponent<RectTransform> ().sizeDelta = new Vector2 (amount * fullWidth, this.GetComponent<RectTransform> ().sizeDelta.y);
		}


}