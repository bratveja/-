using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcAI : MonoBehaviour
{
	public GameObject[] waypoints; // waypoints for the npc to walk
	public GameObject[] waypointsForBreak; // waypoints for the npc to take break
	public GameObject[] waypointsForTalking; // waypoints for the npc to talk
	public Transform rightTrigger; // the triiger of the turning right animation
	public Transform leftTrigger; // the trigger of the runing left animation
	public Transform player; // refrerence to the player transform
	public Transform head; // refference to the npc head transform so he can cast the detection box from his head
	public NavMeshAgent agent; // refference to the navmesh settiang the npc to be agent
	public Animator animator; // reference to the npc animator

	// buttons for the npc to go on talking waypoint and start talking etc
	public Button button1;
	public Button button2;
	public Button button3;
	public Button button4;
	public Button button5;
	public Button button6;
	public Button button7;
	public Button button8;
	public Button button9;
	public Button button10;
	public PauseOptions pause; // reference to the pause script
	public NpcRaycast timerForRestart; // reference to the npc raycast script so we can wait a fixed amout of time on a waypoint for break
	public float timer = 0f; // settign up timer for walkingg around
	private float rotSpeed = 1f; // the rotation speed when we hit waypoijnt
	private float speed = 1.5f; // the speed of the npc
	private float accuracyWaypoints = 1.3f; // how close to pass when we're close to waypiont
	public float timeBeforeBreak; // setting tiem for break
	private float timerForTalking; // how much time the npc to talk
	private int currentWaypoints = 0; // how much points we passed
	private int currentWaypointsForBreak = 0; // how many times we took break
	private int currentWaypointsForTalking = 0; // how many times we went to the talking state

	public string state = "walking"; // state string so we can store info

	// checking if we're walking around
	public bool isOn;
	public bool isOn1;
	public bool isOn2;
	public bool isOn3;
	public bool isOn4;
	public bool isOn5;
	public bool isOn6;
	public bool isOn7;
	public bool isOn8;
	public bool isOn9;

	private bool isActive = false; // checking if the the break waypoints are active 
	private bool isTalkingBool = false; // checking if the talking state is active
	private bool lateStart = false; // calling late start once
	private bool startForOnce = false; // calling start once

	// sorting waypoints by name
	public class waypointsSorter : IComparer
	{
		int IComparer.Compare(System.Object x, System.Object y)
		{
			return ((new CaseInsensitiveComparer()).Compare(((GameObject)x).name, ((GameObject)y).name));
		}

	}

	private void Awake()
	{
		//getting components on awake and generating time for break before start
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		leftTrigger = GameObject.FindGameObjectWithTag("leftTurnTag").GetComponent<Transform>();
		rightTrigger = GameObject.FindGameObjectWithTag("rightTurnTag").GetComponent<Transform>();
		pause = GameObject.FindGameObjectWithTag("Player").GetComponent<PauseOptions>();
		timerForRestart = gameObject.GetComponent<NpcRaycast>();
		timeBeforeBreak = UnityEngine.Random.Range(240f, 600f);
	}

	private void Start()
	{
		//sorting the waypoints for walking
		IComparer myComparer = new waypointsSorter();
		waypoints = GameObject.FindGameObjectsWithTag("waypointsBeforeBreak");
		Array.Sort(waypoints, myComparer);
	}


	private void LateStart()
	{
		//sorting the waypooints for chilling
		IComparer myComparer = new waypointsSorter();
		waypointsForBreak = GameObject.FindGameObjectsWithTag("waypointsForBreak");
		Array.Sort(waypointsForBreak, myComparer);
	}

	private void FixedUpdate()
	{
		//settting the direction of the npc
		Vector3 direction = player.position - this.transform.position;
		//setting the angle of the npc to be albe to detect us
		float angle = Vector3.Angle(direction, head.up);
		direction.y = 0;

		// calling the break function
		timer += Time.deltaTime;
		// checking if the timer is bigger than time before break if it's true we we're setting the waypoints for walk array to be null and starting our brake
		if (timer > timeBeforeBreak)
		{
			//reseting the waypoints for walking and setting the waypoints for break also calling them once cuz F'in performance
			if (lateStart == false)
			{
				LateStart();
				lateStart = true;
				startForOnce = false;
				Array.Resize(ref waypoints, 0);
			}
			// setting the waypoints for walking array to be zero

			// checking if the waypoints array is zero if it's we're going to take break
			if (/*state == "walking" &&*/ waypoints.Length == 0)
			{
				animator.SetBool("isWalking", true);
				animator.SetBool("isIdle", false);
				state = "takingBreak";

				//checking if we've got waypoints for taking break
				if (state == "takingBreak" && waypointsForBreak.Length > 0)
				{
					//picking random waypoint for taking break
					if (isActive == false)
					{
						// picking random waypoint
						currentWaypointsForBreak = UnityEngine.Random.Range(0, waypointsForBreak.Length);
						isActive = true;
					}
					if (timer < timeBeforeBreak)
					{
						isActive = false;
					}
					// tellng the npc to take the closest path to the given waypoint
					agent.SetDestination(waypointsForBreak[currentWaypointsForBreak].transform.position);

					// rortating the npc on the waypoint z axis global
					Vector3 waypointDirection = waypointsForBreak[currentWaypointsForBreak].transform.position - this.transform.position;
					float magnitude = 0.0001f;
					waypointDirection.y = magnitude;
					this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(waypointDirection), rotSpeed * Time.fixedDeltaTime);
				}
			}
		}

		// checking if the timer is smaller number than time before break or the timer for restart is bigger than the break time if it is we're starting action
		else if (timer < timeBeforeBreak || timerForRestart.timer > timerForRestart.breakTime)
		{
			//calling the waypoints for walk function and sorting the way points
			//setting the state to be walking
			state = "walking";
			//settting the waypoints for break array to be zero
			//calling start function once and reseting array for waypointsForBreak
			if (startForOnce == false)
			{
				Start();
				startForOnce = true;
				lateStart = false;
				Array.Resize(ref waypointsForBreak, 0);
			}
			//checking our state and if our waypoints for walking are not zero 
			if (state == "walking" && waypoints.Length > 0)
			{
				animator.SetBool("isIdle", false);
				animator.SetBool("isWalking", true);
				//taking sctrict path 
				if (Vector3.Distance(waypoints[currentWaypoints].transform.position, transform.position) < accuracyWaypoints)
				{
					//strict path
					currentWaypoints++;
					if (currentWaypoints >= waypoints.Length)
					{
						currentWaypoints = 0;
					}
				}
				// telling to the npc to walk on this path
				agent.SetDestination(waypoints[currentWaypoints].transform.position);
			}
		}

		// starting the talking path and talking state
		button1.onClick.AddListener(IsTalking);
		button2.onClick.AddListener(IsTalking1);
		button3.onClick.AddListener(IsTalking2);
		button4.onClick.AddListener(IsTalking3);
		button5.onClick.AddListener(IsTalking4);
		button6.onClick.AddListener(IsTalking5);
		button7.onClick.AddListener(IsTalking6);
		button8.onClick.AddListener(IsTalking7);
		button9.onClick.AddListener(IsTalking8);
		button10.onClick.AddListener(IsTalking9);
		if (isOn == true)
		{
			pause.text.text = "След мен!";
			animator.SetBool("isWalking", true);
			animator.SetBool("isIdle", false);
			state = "talking";
			if (state == "talking" && waypointsForTalking.Length > 0)
			{
				//picking waypoint to talk chosen by the palyers
				if (isTalkingBool == false || timerForTalking < timerForRestart.talkingAudio[0].length)
				{
					currentWaypointsForTalking = 0;
					isTalkingBool = true;
				}
				//checking if the talking time ended
				if (timerForTalking < timerForRestart.talkingAudio[0].length)
				{
					isActive = false;
				}
				// telling to the npc to rotate on the z axis of the waypoint for talking and setting the destination
				agent.SetDestination(waypointsForTalking[currentWaypointsForTalking].transform.position);
				Vector3 waypointDirection = waypointsForTalking[currentWaypointsForTalking].transform.position - this.transform.position;
				float magnitude = 1f;
				waypointDirection.y = magnitude;
				// applying the rotation
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(waypointDirection), rotSpeed * Time.fixedDeltaTime);
			}
		}
		else if (isOn1 == true)
		{
			pause.text.text = "След мен!";
			animator.SetBool("isWalking", true);
			animator.SetBool("isIdle", false);
			state = "talking";
			if (state == "talking" && waypointsForTalking.Length > 0)
			{
					//picking waypoint to talk chosen by the palyers
					if (isTalkingBool == false || timerForTalking < timerForRestart.talkingAudio[1].length)
					{
						currentWaypointsForTalking = 1;
						isTalkingBool = true;
					}
					//checking if the talking time ended
					else if (timerForTalking > timerForRestart.talkingAudio[1].length)
					{
						isOn1 = false;
						isTalkingBool = false;
					}
				// telling to the npc to rotate on the z axis of the waypoint for talking and setting the destination
				agent.SetDestination(waypointsForTalking[currentWaypointsForTalking].transform.position);
				Vector3 waypointDirection = waypointsForTalking[currentWaypointsForTalking].transform.position - this.transform.position;
				float magnitude = 0.0001f;
				waypointDirection.y = magnitude;
				// applying the rotation
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(waypointDirection), rotSpeed * Time.fixedDeltaTime);
			}
		}
		else if (isOn2 == true)
		{
			pause.text.text = "След мен!";
			animator.SetBool("isWalking", true);
			animator.SetBool("isIdle", false);
			state = "talking";
			if (state == "talking" && waypointsForTalking.Length > 0)
			{
				//picking waypoint to talk chosen by the palyers
				if (isTalkingBool == false || timerForTalking < timerForRestart.talkingAudio[2].length)
				{
					currentWaypointsForTalking = 2;
					isTalkingBool = true;
				}
				//checking if the talking time ended
				else if (timerForTalking > timerForRestart.talkingAudio[2].length)
				{
					isOn2 = false;
					isTalkingBool = false;
				}
				// telling to the npc to rotate on the z axis of the waypoint for talking and setting the destination
				agent.SetDestination(waypointsForTalking[currentWaypointsForTalking].transform.position);
				Vector3 waypointDirection = waypointsForTalking[currentWaypointsForTalking].transform.position - this.transform.position;
				float magnitude = 0.0001f;
				waypointDirection.y = magnitude;
				// applying the rotation
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(waypointDirection), rotSpeed * Time.fixedDeltaTime);
			}
		}
		else if (isOn3 == true)
		{
			pause.text.text = "След мен!";
			animator.SetBool("isWalking", true);
			animator.SetBool("isIdle", false);
			state = "talking";
			if (state == "talking" && waypointsForTalking.Length > 0)
			{
					//picking waypoint to talk chosen by the palyers
					if (isTalkingBool == false || timerForTalking < timerForRestart.talkingAudio[3].length)
					{
						currentWaypointsForTalking = 3;
						isTalkingBool = true;
					}
					//checking if the talking time ended
					else if (timerForTalking > timerForRestart.talkingAudio[3].length)
					{
						isOn3 = false;
						isTalkingBool = false;
					}
				// telling to the npc to rotate on the z axis of the waypoint for talking and setting the destination
				agent.SetDestination(waypointsForTalking[currentWaypointsForTalking].transform.position);
				Vector3 waypointDirection = waypointsForTalking[currentWaypointsForTalking].transform.position - this.transform.position;
				float magnitude = 0.0001f;
				waypointDirection.y = magnitude;
				// applying the rotation
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(waypointDirection), rotSpeed * Time.fixedDeltaTime);
			}
		}
		else if (isOn4 == true)
		{
			pause.text.text = "След мен!";
			animator.SetBool("isWalking", true);
			animator.SetBool("isIdle", false);
			state = "talking";
			if (state == "talking" && waypointsForTalking.Length > 0)
			{
					//picking waypoint to talk chosen by the palyers
					if (isTalkingBool == false || timerForTalking < timerForRestart.talkingAudio[4].length)
					{
						currentWaypointsForTalking = 4;
						isTalkingBool = true;
					}
					//checking if the talking time ended
					else if (timerForTalking > timerForRestart.talkingAudio[4].length)
					{
						isOn4 = false;
						isTalkingBool = false;
					}
				// telling to the npc to rotate on the z axis of the waypoint for talking and setting the destination
				agent.SetDestination(waypointsForTalking[currentWaypointsForTalking].transform.position);
				Vector3 waypointDirection = waypointsForTalking[currentWaypointsForTalking].transform.position - this.transform.position;
				float magnitude = 0.0001f;
				waypointDirection.y = magnitude;
				// applying the rotation
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(waypointDirection), rotSpeed * Time.fixedDeltaTime);
			}
		}
		else if (isOn5 == true)
		{
			pause.text.text = "След мен!";
			animator.SetBool("isWalking", true);
			animator.SetBool("isIdle", false);
			state = "talking";
			if (state == "talking" && waypointsForTalking.Length > 0)
			{
					//picking waypoint to talk chosen by the palyers
					if (isTalkingBool == false || timerForTalking < timerForRestart.talkingAudio[5].length)
					{
						currentWaypointsForTalking = 5;
						isTalkingBool = true;
					}
					//checking if the talking time ended
					else if (timerForTalking > timerForRestart.talkingAudio[5].length)
					{
						isOn5 = false;
						isTalkingBool = false;
					}
				// telling to the npc to rotate on the z axis of the waypoint for talking and setting the destination
				agent.SetDestination(waypointsForTalking[currentWaypointsForTalking].transform.position);
				Vector3 waypointDirection = waypointsForTalking[currentWaypointsForTalking].transform.position - this.transform.position;
				float magnitude = 0.0001f;
				waypointDirection.y = magnitude;
				// applying the rotation
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(waypointDirection), rotSpeed * Time.fixedDeltaTime);
			}
		}
		else if (isOn6 == true)
		{
			pause.text.text = "След мен!";
			animator.SetBool("isWalking", true);
			animator.SetBool("isIdle", false);
			state = "talking";
			if (state == "talking" && waypointsForTalking.Length > 0)
			{
					//picking waypoint to talk chosen by the palyers
					if (isTalkingBool == false || timerForTalking < timerForRestart.talkingAudio[6].length)
					{
						currentWaypointsForTalking = 6;
						isTalkingBool = true;
					}
					//checking if the talking time ended
					else if (timerForTalking > timerForRestart.talkingAudio[6].length)
					{
						isOn6 = false;
						isTalkingBool = false;
					}
				// telling to the npc to rotate on the z axis of the waypoint for talking and setting the destination
				agent.SetDestination(waypointsForTalking[currentWaypointsForTalking].transform.position);
				Vector3 waypointDirection = waypointsForTalking[currentWaypointsForTalking].transform.position - this.transform.position;
				float magnitude = 0.0001f;
				waypointDirection.y = magnitude;
				// applying the rotation
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(waypointDirection), rotSpeed * Time.fixedDeltaTime);
			}
		}
		else if (isOn7 == true)
		{
			pause.text.text = "След мен!";
			animator.SetBool("isWalking", true);
			animator.SetBool("isIdle", false);
			state = "talking";
			if (state == "talking" && waypointsForTalking.Length > 0)
			{
					//picking waypoint to talk chosen by the palyers
					if (isTalkingBool == false || timerForTalking < timerForRestart.talkingAudio[7].length)
					{
						currentWaypointsForTalking = 7;
						isTalkingBool = true;
					}
					//checking if the talking time ended
					else if (timerForTalking > timerForRestart.talkingAudio[7].length)
					{
						isOn7 = false;
						isTalkingBool = false;
					}
				// telling to the npc to rotate on the z axis of the waypoint for talking and setting the destination
				agent.SetDestination(waypointsForTalking[currentWaypointsForTalking].transform.position);
				Vector3 waypointDirection = waypointsForTalking[currentWaypointsForTalking].transform.position - this.transform.position;
				float magnitude = 0.0001f;
				waypointDirection.y = magnitude;
				// applying the rotation
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(waypointDirection), rotSpeed * Time.fixedDeltaTime);
			}
		}
		else if (isOn8 == true)
		{
			pause.text.text = "След мен!";
			animator.SetBool("isWalking", true);
			animator.SetBool("isIdle", false);
			state = "talking";
			if (state == "talking" && waypointsForTalking.Length > 0)
			{
				//picking waypoint to talk chosen by the palyers
				if (isTalkingBool == false || timerForTalking < timerForRestart.talkingAudio[8].length)
				{
					currentWaypointsForTalking = 8;
					isTalkingBool = true;
				}
				//checking if the talking time ended
				else if (timerForTalking > timerForRestart.talkingAudio[8].length)
				{
					isOn8 = false;
					isTalkingBool = false;
				}
				// telling to the npc to rotate on the z axis of the waypoint for talking and setting the destination
				agent.SetDestination(waypointsForTalking[currentWaypointsForTalking].transform.position);
				Vector3 waypointDirection = waypointsForTalking[currentWaypointsForTalking].transform.position - this.transform.position;
				float magnitude = 0.0001f;
				waypointDirection.y = magnitude;
				// applying the rotation
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(waypointDirection), rotSpeed * Time.fixedDeltaTime);
			}
		}
		else if (isOn9 == true)
		{
			pause.text.text = "След мен!";
			animator.SetBool("isWalking", true);
			animator.SetBool("isIdle", false);
			state = "talking";
			if (state == "talking" && waypointsForTalking.Length > 0)
			{
					//picking waypoint to talk chosen by the palyers
					if (isTalkingBool == false || timerForTalking < timerForRestart.talkingAudio[9].length)
					{
						currentWaypointsForTalking = 9;
						isTalkingBool = true;
					}
					//checking if the talking time ended
					else if (timerForTalking > timerForRestart.talkingAudio[9].length)
					{
						isOn9 = false;
						isTalkingBool = false;
					}
				// telling to the npc to rotate on the z axis of the waypoint for talking and setting the destination
				agent.SetDestination(waypointsForTalking[currentWaypointsForTalking].transform.position);
				Vector3 waypointDirection = waypointsForTalking[currentWaypointsForTalking].transform.position - this.transform.position;
				float magnitude = 0.0001f;
				waypointDirection.y = magnitude;
				// applying the rotation
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(waypointDirection), rotSpeed * Time.fixedDeltaTime);
			}
		}

		// checking we're close to the npc and the anlge if evryting is true we're activating the idle state 
		if (Vector3.Distance(player.position, this.transform.position) < 3 && (-angle < 50 || state == "looking"))
		{
			//setting the npc path to be false and setting the state to be looking
			agent.isStopped = true;
			state = "looking";

			// if we're close the to the npc we're activating the idle anim
			if (direction.magnitude > 2)
			{
				animator.SetBool("isIdle", true);
				animator.SetBool("isWalking", false);
				animator.SetBool("isTurningRight", false);
				animator.SetBool("isTurningLeft", false);
			}
			else
			{
				//turning the npc left or right from different script
				animator.SetBool("isIdle", false);
				animator.SetBool("isWalking", false);
				//this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
			}
		}
		else
		{
			//setting everying to be default
			agent.isStopped = false;
			if (timer > timeBeforeBreak)
			{
				state = "takingBreak";
			}
			else
			{
				state = "walking";
			}

		}
	}

	//checking if we're talking
	private void IsTalking()
	{
		isOn = true;
	}

	private void IsTalking1()
	{
		isOn1 = true;
	}

	private void IsTalking2()
	{
		isOn2 = true;
	}

	private void IsTalking3()
	{
		isOn3 = true;
	}

	private void IsTalking4()
	{
		isOn4 = true;
	}

	private void IsTalking5()
	{
		isOn5 = true;
	}

	private void IsTalking6()
	{
		isOn6 = true;
	}

	private void IsTalking7()
	{
		isOn7 = true;
	}

	private void IsTalking8()
	{
		isOn8 = true;
	}

	private void IsTalking9()
	{
		isOn9 = true;
	}

	//// telling the npc to go to room 1
	//private void Button1()
	//{
	//	currentWaypointsForTalking = 0;
	//}
	//// telling the npc to go to room 2
	//private void Button2()
	//{
	//	currentWaypointsForTalking = 1;
	//}
}

#region npcLookingAtPlayer
//if (Vector3.Distance(player.position, this.transform.position) < 3 && (-angle < 50 || state == "looking"))
//{
//	agent.isStopped = true;
//	Debug.Log("agent stopped");
//	state = "looking";

//	if (direction.magnitude > 2)
//	{
//		animator.SetBool("isIdle", true);
//		animator.SetBool("isWalking", false);
//		animator.SetBool("isTurningRight", false);
//		animator.SetBool("isTurningLeft", false);
//	}
//	else
//	{
//		animator.SetBool("isIdle", false);
//		animator.SetBool("isWalking", false);
//		//this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
//	}
//}
//else
//{
//	agent.isStopped = false;
//	animator.SetBool("isWalking", true);
//	animator.SetBool("isIdle", false);
//	state = "takingBreak";
//}
#endregion
#region takingBreak
//Vector3 direction = player.position - this.transform.position;
//float angle = Vector3.Angle(direction, head.up);
//direction.y = 0;
//Array.Resize(ref waypoints, 0);
//if (state == "takingBreak" && waypointsForBreak.Length > 0)
//{
//	if (Vector3.Distance(waypointsForBreak[currentWaypointsForBreak].transform.position, transform.position) < accuracyWaypoints)
//	{
//		//picking random waypoint for taking breake
//		currentWaypointsForBreak = UnityEngine.Random.Range(0, waypointsForBreak.Length);

//		if (currentWaypointsForBreak >= waypointsForBreak.Length)
//		{
//			currentWaypointsForBreak = 0;
//		}
//	}
//	agent.SetDestination(waypointsForBreak[currentWaypointsForBreak].transform.position);

//	if (Vector3.Distance(player.position, this.transform.position) < 3 && (-angle < 50 || state == "looking"))
//	{
//		agent.isStopped = true;
//		state = "looking";

//		if (direction.magnitude > 2)
//		{
//			animator.SetBool("isIdle", true);
//			animator.SetBool("isWalking", false);
//			animator.SetBool("isTurningRight", false);
//			animator.SetBool("isTurningLeft", false);
//		}
//		else
//		{
//			animator.SetBool("isIdle", false);
//			animator.SetBool("isWalking", false);
//			//this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
//		}
//	}
//	else
//	{
//		agent.isStopped = false;
//		animator.SetBool("isWalking", true);
//		animator.SetBool("isIdle", false);
//		state = "walking";
//	}
//}
//yield return null;
//}
#endregion
#region strictPathWaypoints
//strict path
//currentWaypoints++;
//if (currentWaypoints >= waypoints.Length)
//{
//	currentWaypoints = 0;
//}
#endregion
#region backupOfTrigger
//private void OnCollisionStay(Collision col)
//{
//	if (col.gameObject.tag == "Player" && player.transform.position == leftTrigger.transform.position)
//	{
//		Debug.Log("Turning left");
//		anim.SetBool("isIdle", false);
//		anim.SetBool("isWalking", false);
//		anim.SetBool("isTurningLeft", true);
//	}
//	else if (col.gameObject.name == "Player" && player.transform.position == rightTrigger.transform.position)
//	{
//		Debug.Log("Turning right");
//		anim.SetBool("isIdle", false);
//		anim.SetBool("isWalking", false);
//		anim.SetBool("isTurningRight", true);
//	}
//	else
//	{
//		Debug.Log("Middle Part");
//		anim.SetBool("isIdle", true);
//		anim.SetBool("isTurningLeft", false);
//		anim.SetBool("isTurningRight", false);
//		anim.SetBool("isWalking", false);
//	}
//}
#endregion