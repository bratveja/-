using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScript : MonoBehaviour
{

	public void LoadLocationForCourtyard()
	{
		PlayerPrefs.GetFloat("pPosX");
		PlayerPrefs.GetFloat("pPosY");
		PlayerPrefs.GetFloat("pPosZ");
	}

	public void LoadLocationForFloor()
	{
		PlayerPrefs.GetFloat("pPosX");
		PlayerPrefs.GetFloat("pPosY");
		PlayerPrefs.GetFloat("pPosZ");

		PlayerPrefs.GetFloat("gPosX");
		PlayerPrefs.GetFloat("gPosY");
		PlayerPrefs.GetFloat("gPosZ");
	}
}
