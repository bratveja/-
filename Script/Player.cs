using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
	public float movementSpeed = 4f; // the movement speed of the character 
	public float sprintSpeed = 6f; // the sprint movementSpeed
	public AudioSource controller;
	public AudioClip tilesSound1;
	public AudioClip tilesSound2;
	public AudioClip dirtSound;
	public AudioClip waterSound;
	public AudioClip woodenStepsSound;

	private float rayLenght = 4f;
	private bool isSprinting = false;
	private bool isSoundPlaying = true;

	private void Update()
	{
		if (Input.GetButtonDown("Shift"))
		{
			isSprinting = true;
		}
		else if (Input.GetButtonUp("Shift"))
		{
			isSprinting = false;
		}

		if (!isSprinting)
		{
			// setting the movement on the vertical axis
			float translation = Input.GetAxis("Vertical") * movementSpeed;
			// setting the movement on the horizontal axis
			float strafe = Input.GetAxis("Horizontal") * movementSpeed;
			// multiplying the movement every different frame with the real time
			translation *= Time.deltaTime;
			strafe *= Time.deltaTime;

			// moving the character
			transform.Translate(strafe, 0, translation);

			WalkingSounds();
		}
		else if (isSprinting)
		{
			// setting the movement on the vertical axis
			float translation = Input.GetAxis("Vertical") * sprintSpeed;
			// setting the movement on the horizontal axis
			float strafe = Input.GetAxis("Horizontal") * sprintSpeed;
			// multiplying the movement every different frame with the real time
			translation *= Time.deltaTime;
			strafe *= Time.deltaTime;

			// moving the character
			transform.Translate(strafe, 0, translation);

			WalkingSounds();
		}

	}

	// checking for raycast collision and giving the desiered audio clip
	private void WalkingSounds()
	{
		Vector3 ray = -Vector3.up;
		RaycastHit hit;

		if (Physics.Raycast(transform.position, ray, out hit, rayLenght))
		{
			if (hit.collider.tag == "tilesOutside")
			{
				controller.clip = tilesSound1;
			}
			else if (hit.collider.tag == "dirtSoundPlayer")
			{
				controller.clip = dirtSound;
			}
			else if (hit.collider.tag == "WoodSound")
			{
				controller.clip = woodenStepsSound;
			}
			else if (hit.collider.tag == "Tiles2Sound")
			{
				controller.clip = tilesSound2;
			}

			if (Input.GetKeyDown(KeyCode.W))
			{
				controller.loop = true;
				controller.Play();
			}
			else if (Input.GetKeyUp(KeyCode.W))
			{
				controller.loop = false;
				controller.Stop();
			}
			else if (Input.GetKeyDown(KeyCode.A))
			{
				controller.Play();
			}
			else if (Input.GetKeyDown(KeyCode.S))
			{
				controller.Play();
			}
			else if (Input.GetKeyDown(KeyCode.D))
			{
				controller.Play();
			}

		}
	}
}

#region testCharacterController
//[SerializeField] private float movementSpeed = 5f; // movement speed of the player
//[SerializeField] private float lookSensitivity = 5f; // the mouse sensitivity
//[SerializeField] private float camLockRotation = 85f; // the lock rotation of the camera
//[SerializeField] private Options options; // options refference

//private float cameraRotX = 0f; // setting the x rotation of the camera
//private float rotationX = 0f; // setting the x rotation of the player
//private float rayLength = 1f; // the ray casted is setted to be 1
//private Vector3 rotation = Vector3.zero; // setting the rotation of the player to be zero on the vector 3
//private Vector3 velocity = Vector3.zero; // setting the velocity of the player to be zero on the vector 3 
//private Camera playerCamera; // giving the player a camera
//private ObjectSelection objSelect; // refference to the objSelection script
//private Rigidbody rb; // refference to the player righidbody 

//private bool isInverted;

//private void Start()
//{
//	//lookSensitivity = sensitivitySlider.value;
//	playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
//	objSelect = GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectSelection>();
//	rb = GetComponent<Rigidbody>();
//}

//private void Update()
//{
//	//player movment velocity as a vector and getting the inputs from the users keyobard
//	float _xMove = Input.GetAxisRaw("Horizontal");
//	float _zMove = Input.GetAxisRaw("Vertical");

//	// telling the player to move on the vector 3 variables
//	Vector3 _moveHorizontal = transform.right * _xMove;
//	Vector3 _moveVertical = transform.forward * _zMove;

//	//movement vector combination
//	Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * movementSpeed;

//	Move(_velocity);

//	//camera rotation yRotation
//	float _yRot = Input.GetAxisRaw("Mouse X");

//	Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

//	//rotation applyer
//	Rotate(_rotation);

//	//camera rotation xRotation
//	float _xRot = Input.GetAxisRaw("Mouse Y");

//	float _rotationX = _xRot * lookSensitivity;

//	//rotation applyer
//	RotateX(_rotationX);
//}

////get movement vector
//private void Move(Vector3 _velocity)
//{
//	velocity = _velocity;
//}

////rotation method
//private void Rotate(Vector3 _rotation)
//{
//	rotation = _rotation;
//}

//private void RotateX(float _rotationX)
//{
//	rotationX = _rotationX;
//}

////run on a fixed time
//private void FixedUpdate()
//{
//	PerformMovement();
//	if (options.invertMouseToggle == true)
//	{
//		PerformRotatiom();
//	}
//	else if (options.invertMouseToggle == false)
//	{
//		PerformInvertedRotation();
//	}
//}

//// moving the player
//private void PerformMovement()
//{
//	if (velocity != Vector3.zero)
//	{
//		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
//	}
//}

////rotating the camera
//private void PerformRotatiom()
//{
//	rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
//	// locking the camera rot
//	if (playerCamera != null)
//	{
//		cameraRotX -= rotationX;
//		cameraRotX = Mathf.Clamp(cameraRotX, -camLockRotation, camLockRotation);

//		playerCamera.transform.localEulerAngles = new Vector3(cameraRotX, 0f, 0f);
//	}
//}

//private void PerformInvertedRotation()
//{
//	rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
//	// locking the camera rot
//	if (playerCamera != null)
//	{
//		cameraRotX += rotationX;
//		cameraRotX = Mathf.Clamp(cameraRotX, -camLockRotation, camLockRotation);

//		playerCamera.transform.localEulerAngles = new Vector3(cameraRotX, 0f, 0f);
//	}
//}
#endregion
