using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public GameObject player; // the player gameObject
	public Options mouseController; // refference to the options script so we can invers the mouse
	public Camera playerCam; // refference to the player cam
	public GameObject distance1; // the distance between the player and the gameObject for calculating when to render the objects
	public GameObject distance2;
	public GameObject[] floorRight; // the gameObjects to be rendered when need to
	public GameObject[] floorLeft;
	public float mouseSensitivity = 5f; // the mouse sensitivity it's public because will be acessed by the options script
	public float dist1; // the vector 2 distance beteween the player and the distance object == rightPart
	public float dist2; // == leftPart
	private float smoothingCameraMovement = 1f; // smoothing the camera movement low means spike movements
	private Vector2 mouseLook; // the vector used for the camera look
	private Vector2 smoothVector; // the vector used for smoothing the camera

	public bool isInverted; // checking if the mouse is inverted

	private void Start()
	{
		//setting the mouse invert value and speed
		isInverted = mouseController.invertMouseToggle.isOn;
		mouseSensitivity = mouseController.sensitivitySlider.value;
	}

	private void Update()
	{

		// getting the axis input
		var mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		// setting the axis input
		mouseDirection = Vector2.Scale(mouseDirection, new Vector2(mouseSensitivity * smoothingCameraMovement, mouseSensitivity * smoothingCameraMovement));
		// smoothing the camera
		smoothVector.x = Mathf.Lerp(smoothVector.x, mouseDirection.x, 1f / smoothingCameraMovement);
		smoothVector.y = Mathf.Lerp(smoothVector.y, mouseDirection.y, 1f / smoothingCameraMovement);
		mouseLook += smoothVector;

		// locking the camera
		mouseLook.y = Mathf.Clamp(mouseLook.y, -85f, 85f);

		// applying the camera rot on the player local axis rotation on the y and checking if it's inveted
		if (isInverted)
		{
			transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		}
		else
		{
			transform.localRotation = Quaternion.AngleAxis(mouseLook.y, Vector3.right);
		}

		// applying the camera rot on the player local axis y
		player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);

		RightPart();
		LeftPart();
	}

	private void RightPart()
	{
		LayerMask mask = 13;
		for (int i = 0; i < floorRight.Length; i++)
		{
			floorRight[i].layer = mask;
			dist1 = Vector3.Distance(distance1.transform.position, gameObject.transform.position);

			if (dist1 < 32f)
			{
				playerCam.cullingMask = (1 << LayerMask.NameToLayer("rightRoomObjects")) |
				(1 << LayerMask.NameToLayer("Default")) |
				(1 << LayerMask.NameToLayer("TransparentFX")) |
				(1 << LayerMask.NameToLayer("Ignore Raycast")) |
				(1 << LayerMask.NameToLayer("Water")) |
				(1 << LayerMask.NameToLayer("UI")) |
				(1 << LayerMask.NameToLayer("walls")) |
				(1 << LayerMask.NameToLayer("lights")) |
				(1 << LayerMask.NameToLayer("floor")) |
				(1 << LayerMask.NameToLayer("PC")) |
				(0 << LayerMask.NameToLayer("leftRoomObjects"));
			}
			else
			{
				playerCam.cullingMask = (0 << LayerMask.NameToLayer("rightRoomObjects")) |
				(1 << LayerMask.NameToLayer("Default")) |
				(1 << LayerMask.NameToLayer("TransparentFX")) |
				(1 << LayerMask.NameToLayer("Ignore Raycast")) |
				(1 << LayerMask.NameToLayer("Water")) |
				(1 << LayerMask.NameToLayer("UI")) |
				(1 << LayerMask.NameToLayer("walls")) |
				(1 << LayerMask.NameToLayer("lights")) |
				(1 << LayerMask.NameToLayer("floor")) |
				(1 << LayerMask.NameToLayer("PC")) |
				(1 << LayerMask.NameToLayer("leftRoomObjects"));
			}
		}
	}

	private void LeftPart()
	{
		LayerMask mask = 14;
		for (int b = 0; b < floorLeft.Length; b++)
		{
			floorLeft[b].layer = mask;
			dist2 = Vector3.Distance(distance2.transform.position, gameObject.transform.position);

			if (dist2 < 32f)
			{
				playerCam.cullingMask = (1 << LayerMask.NameToLayer("leftRoomObjects")) |
				(1 << LayerMask.NameToLayer("Default")) |
				(1 << LayerMask.NameToLayer("TransparentFX")) |
				(1 << LayerMask.NameToLayer("Ignore Raycast")) |
				(1 << LayerMask.NameToLayer("Water")) |
				(1 << LayerMask.NameToLayer("UI")) |
				(1 << LayerMask.NameToLayer("walls")) |
				(1 << LayerMask.NameToLayer("lights")) |
				(1 << LayerMask.NameToLayer("floor")) |
				(1 << LayerMask.NameToLayer("PC")) |
				(0 << LayerMask.NameToLayer("rightRoomObjects"));
			}
			else
			{
				playerCam.cullingMask = (0 << LayerMask.NameToLayer("leftRoomObjects")) |
				(1 << LayerMask.NameToLayer("Default")) |
				(1 << LayerMask.NameToLayer("TransparentFX")) |
				(1 << LayerMask.NameToLayer("Ignore Raycast")) |
				(1 << LayerMask.NameToLayer("Water")) |
				(1 << LayerMask.NameToLayer("UI")) |
				(1 << LayerMask.NameToLayer("walls")) |
				(1 << LayerMask.NameToLayer("lights")) |
				(1 << LayerMask.NameToLayer("floor")) |
				(1 << LayerMask.NameToLayer("PC")) |
				(1 << LayerMask.NameToLayer("rightRoomObjects"));
			}
		}
	}
}