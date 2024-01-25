using UnityEngine;
using System;

public class MinimapClock : MonoBehaviour
{
	//the transform of the clock sticks
	public Transform minutes;
	public Transform hours;

	private void Update()
	{
		//getting the user pc time and setting it to be as the time of the game
		DateTime currentTime = DateTime.Now;
		float minutesF = (float) currentTime.Minute;
		float hoursF = (float) currentTime.Hour;

		float minutesAngle = -360 * (minutesF/60);
		float hoursAngle = -360 * (hoursF / 12);

		minutes.localRotation = Quaternion.Euler(0, 0, minutesAngle);
		hours.localRotation = Quaternion.Euler(0, 0, hoursAngle);
	}

}
