using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Transform playerTransform;

	void LateUpdate ()
	{
		if (playerTransform != null)
		{
			var myPosition = this.transform.position;
			myPosition.x = playerTransform.position.x;
			myPosition.y = playerTransform.position.y;

			this.transform.position = myPosition;
		}
	}
}
