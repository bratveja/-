using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSript : MonoBehaviour
{
	public GameObject playerPosition;
	public GameObject gidPosition;


	private void SavePlayerLocationCal()
	{
		PlayerPrefs.SetFloat("pPosX", playerPosition.transform.position.x);
		PlayerPrefs.SetFloat("pPosY", playerPosition.transform.position.y);
		PlayerPrefs.SetFloat("pPosZ", playerPosition.transform.position.z);
	}

	private void SaveGidLocationCal()
	{
		PlayerPrefs.SetFloat("gPosX", gidPosition.transform.position.x);
		PlayerPrefs.SetFloat("gPosY", gidPosition.transform.position.y);
		PlayerPrefs.SetFloat("gPosZ", gidPosition.transform.position.z);

	}

	public void SavePlayerLocation()
	{
		SavePlayerLocationCal();
	}

	public void SaveGidLocation()
	{
		SaveGidLocationCal();
	}
}
