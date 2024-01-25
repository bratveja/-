using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2 : MonoBehaviour
{

	public GameObject player; // the player gameObject
	public Camera playerCam; // refference to the player cam
	public GameObject distance1;

	// the objects to draw
	#region objectsToDrawArrays
	public GameObject[] room2Objs;
	#endregion

	private void Update()
	{
		Room1Objs();
	}

	private void Room1Objs()
	{
		LayerMask mask = 13;
		int oldCullingMask = playerCam.cullingMask;
		for (int i = 0; i < room2Objs.Length; i++)
		{
				room2Objs[i].layer = mask;
				float dist = Vector3.Distance(distance1.transform.position, gameObject.transform.position);

				if (dist <= 5f)
				{
					playerCam.cullingMask = (1 << LayerMask.NameToLayer("TransparentFX")) |
						(1 << LayerMask.NameToLayer("Default")) |
						(1 << LayerMask.NameToLayer("Ignore Raycast")) |
						(1 << LayerMask.NameToLayer("Water")) |
						(1 << LayerMask.NameToLayer("UI")) |
						(1 << LayerMask.NameToLayer("walls")) |
						(1 << LayerMask.NameToLayer("lights")) |
						(1 << LayerMask.NameToLayer("floor")) |
						(1 << LayerMask.NameToLayer("PC")) |
						(1 << LayerMask.NameToLayer("objectsToRender"));
				}
				else if (dist > 5f)
				{
					playerCam.cullingMask = (1 << LayerMask.NameToLayer("TransparentFX")) |
						(1 << LayerMask.NameToLayer("Default")) |
						(1 << LayerMask.NameToLayer("Ignore Raycast")) |
						(1 << LayerMask.NameToLayer("Water")) |
						(1 << LayerMask.NameToLayer("UI")) |
						(1 << LayerMask.NameToLayer("walls")) |
						(1 << LayerMask.NameToLayer("lights")) |
						(1 << LayerMask.NameToLayer("floor")) |
						(1 << LayerMask.NameToLayer("PC"));
				}
			}
		}
	}