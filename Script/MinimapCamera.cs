using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
	public Transform target;

	//moving the camera with the player
	private void LateUpdate()
	{
		transform.position = new Vector3(target.position.x, transform.position.y, target.transform.position.z);
	}
}