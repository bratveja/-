using UnityEngine;

public class NpcRaycast : MonoBehaviour
{
	[SerializeField]
	private NpcAI aiTrigger; // refference to the ai script
	[SerializeField]
	private float rayLength = 0.5f; // setting ray lenght

	public Animator animator; // reference to the ai animator
	public AudioClip[] talkingAudio; // reference to tho the audio to be played
	public PauseOptions pause;
	public float talkingTimer = 0f; // the timer to be played when the npc is tlaking
	public float talkingTime; // how much the npc should wait the value will be setted to the talking audio lenght
	public float timer = 0f; // setting tiemr
	public float breakTime; // setting break timer

	private AudioSource sourceOfAudio;

	public bool rangeNumberForTalking = false;  // checking if we're in range by defayult is false
	private bool rangeNumber = false; // checking if we're in range

	private void Start()
	{
		sourceOfAudio = GetComponent<AudioSource>();
		aiTrigger = GameObject.Find("gidTrial").GetComponent<NpcAI>();
		//NPC = gameObject;
	}

	void Update()
	{
		//casting ray
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength))
		{
			
			//reseting the bool so there is no repeat in the audio while hitting multiply times
			if(hit.collider.tag == "forResetingTalking")
			{
				rangeNumberForTalking = false;
			}

			//NPC.transform.rotation = this.transform.rotation;
			//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
			//checking if we hit a waypoint for break
			if (hit.collider.tag == "waypointsForBreak")
			{
				// if we did we're getting random float number for break
				if (rangeNumber == false)
				{
					breakTime = Random.Range(60f, 240f);
					rangeNumber = true;
				}
				// till the timer is smaller number we're tkaing break
				timer += Time.deltaTime;
				if (timer < breakTime)
				{
					aiTrigger.animator.SetBool("isIdle", true);
					aiTrigger.animator.SetBool("isTurningLeft", false);
					aiTrigger.animator.SetBool("isTurningRight", false);
					aiTrigger.animator.SetBool("isWalking", false);
					aiTrigger.state = "takingBreak";
				}
				// if the tiemr is bigger nubmer we're starting to walk again
				else if (timer > breakTime)
				{
					if (rangeNumber == true)
					{
						timer = 0;
						aiTrigger.timer = 0;
						rangeNumber = false;
					}
					aiTrigger.animator.SetBool("isIdle", false);
					aiTrigger.animator.SetBool("isTurningLeft", false);
					aiTrigger.animator.SetBool("isTurningRight", false);
					aiTrigger.animator.SetBool("isWalking", true);
					aiTrigger.state = "walking";
				}
			}
			// checking if we're at a waypoint and stating the timer
			else if (hit.collider.tag == "waypointsForTalking1" || hit.collider.tag == "waypointsForTalking2" || hit.collider.tag == "waypointsForTalking3" || hit.collider.tag == "waypointsForTalking4" ||
				hit.collider.tag == "waypointsForTalking5" || hit.collider.tag == "waypointsForTalking6" || hit.collider.tag == "waypointsForTalking7" || hit.collider.tag == "waypointsForTalking8" ||
				hit.collider.tag == "waypointsForTalking9" || hit.collider.tag == "waypointsForTalking10")
			{
				pause.text.text = " ";
				//NPC.transform.rotation = this.transform.rotation;
				if (rangeNumberForTalking == false && hit.collider.tag == "waypointsForTalking1")
				{
					// setting the brake time to be as long as the audio lenght
					aiTrigger.timer = 0;
					talkingTime = talkingAudio[0].length;
					sourceOfAudio.clip = talkingAudio[0];
					sourceOfAudio.Play();
					rangeNumberForTalking = true;
				}
				else if (rangeNumberForTalking == false && hit.collider.tag == "waypointsForTalking2")
				{
					// setting the brake time to be as long as the audio lenght
					aiTrigger.timer = 0;
					talkingTime = talkingAudio[1].length;
					sourceOfAudio.clip = talkingAudio[1];
					sourceOfAudio.Play();
					rangeNumberForTalking = true;
				}
				else if (rangeNumberForTalking == false && hit.collider.tag == "waypointsForTalking3")
				{
					// setting the brake time to be as long as the audio lenght
					aiTrigger.timer = 0;
					talkingTime = talkingAudio[2].length;
					sourceOfAudio.clip = talkingAudio[2];
					sourceOfAudio.Play();
					rangeNumberForTalking = true;
				}
				else if (rangeNumberForTalking == false && hit.collider.tag == "waypointsForTalking4")
				{
					// setting the brake time to be as long as the audio lenght
					aiTrigger.timer = 0;
					talkingTime = talkingAudio[3].length;
					sourceOfAudio.clip = talkingAudio[3];
					sourceOfAudio.Play();
					rangeNumberForTalking = true;
				}
				else if (rangeNumberForTalking == false && hit.collider.tag == "waypointsForTalking5")
				{
					// setting the brake time to be as long as the audio lenght
					aiTrigger.timer = 0;
					talkingTime = talkingAudio[4].length;
					sourceOfAudio.clip = talkingAudio[4];
					sourceOfAudio.Play();
					rangeNumberForTalking = true;
				}
				else if (rangeNumberForTalking == false && hit.collider.tag == "waypointsForTalking6")
				{
					// setting the brake time to be as long as the audio lenght
					aiTrigger.timer = 0;
					talkingTime = talkingAudio[5].length;
					sourceOfAudio.clip = talkingAudio[5];
					sourceOfAudio.Play();
					rangeNumberForTalking = true;
				}
				else if (rangeNumberForTalking == false && hit.collider.tag == "waypointsForTalking7")
				{
					// setting the brake time to be as long as the audio lenght
					aiTrigger.timer = 0;
					talkingTime = talkingAudio[6].length;
					sourceOfAudio.clip = talkingAudio[6];
					sourceOfAudio.Play();
					rangeNumberForTalking = true;
				}
				else if (rangeNumberForTalking == false && hit.collider.tag == "waypointsForTalking8")
				{
					// setting the brake time to be as long as the audio lenght
					aiTrigger.timer = 0;
					talkingTime = talkingAudio[7].length;
					sourceOfAudio.clip = talkingAudio[7];
					sourceOfAudio.Play();
					rangeNumberForTalking = true;
				}
				else if (rangeNumberForTalking == false && hit.collider.tag == "waypointsForTalking9")
				{
					// setting the brake time to be as long as the audio lenght
					aiTrigger.timer = 0;
					talkingTime = talkingAudio[8].length;
					sourceOfAudio.clip = talkingAudio[8];
					sourceOfAudio.Play();
					rangeNumberForTalking = true;
				}
				else if (rangeNumberForTalking == false && hit.collider.tag == "waypointsForTalking10")
				{
					// setting the brake time to be as long as the audio lenght
					aiTrigger.timer = 0;
					talkingTime = talkingAudio[9].length;
					sourceOfAudio.clip = talkingAudio[9];
					sourceOfAudio.Play();
					rangeNumberForTalking = true;
				}

				// checking if the tiemr is is smaller than break time if it is we're playing the talk animation and setting the state to be talkingIdle
				talkingTimer += Time.deltaTime;
				if (talkingTimer < talkingTime)
				{
					aiTrigger.animator.SetBool("isIdle", true);
					aiTrigger.animator.SetBool("isTurningLeft", false);
					aiTrigger.animator.SetBool("isTurningRight", false);
					aiTrigger.animator.SetBool("isWalking", false);
					aiTrigger.state = "talking";
				}

				// if the timer is bigger we're settting everyting to default and the state is begging to be walk again
				else if (talkingTimer >= talkingTime)
				{
					if (rangeNumberForTalking == true)
					{
						talkingTimer = 0;
						aiTrigger.timer = 0;
						aiTrigger.isOn = false;
						aiTrigger.isOn1 = false;
						aiTrigger.isOn2 = false;
						aiTrigger.isOn3 = false;
						aiTrigger.isOn4 = false;
						aiTrigger.isOn5 = false;
						aiTrigger.isOn6 = false;
						aiTrigger.isOn7 = false;
						aiTrigger.isOn8 = false;
						aiTrigger.isOn9 = false;
					}
					aiTrigger.animator.SetBool("isIdle", false);
					aiTrigger.animator.SetBool("isTurningLeft", false);
					aiTrigger.animator.SetBool("isTurningRight", false);
					aiTrigger.animator.SetBool("isWalking", true);
					aiTrigger.state = "walking";
				}
			}
		}
	}
}