using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(TutorialSave))]
public class PauseOptions : MonoBehaviour
{

	public GameObject pauseUi; // the pause ui
	public GameObject miniMap; // minimap ui
	public GameObject decisionsTextPanel; // decision ui
	public GameObject dialougePanel; // dialouge ui
	public GameObject panelForPc; // the pc asking panel
	public GameObject options; // options menu on playmode
	public GameObject[] buttonsToDeactivate; // buttons for next page
	public GameObject[] buttonsToActivate; // buttons for prev page
	public Camera pcCamera; // the camera for the pc in the coridor
	public Camera playerCamera; // player camera
	public Player player; // player script reference
	public AudioSource gidAudio; // soundWhenHitted
	public CameraController cam; // refference to the camera controller script
	public Text textForPc; // thext for displaying if we're close to the pc
	public GameObject panelTutorial; // the tutorail canvas
	public Text playerTutText; // the tutorial text 
	public GameObject buttonTutorial; // the tutorial  button
	public GameObject buttonTutorial2;
	public GameObject buttonTutorial3;
	public GameObject buttonTutorial4;
	public GameObject buttonTutorial5;
	public GameObject buttonTutorial6;
	public GameObject buttonTutorial7;
	public GameObject buttonTutorial8;
	public GameObject buttonTutorial9;
	public GameObject buttonTutorial10;
	public TutorialSave saveWithJ; // saving with json utility
	public ObjectSelection ObjSelection; // reference to the objSelect script
	private NpcAI aiTrigger; // reference to the aiTrigger script
	public Text text; // text for displaying choice

	// checking if the pcCamera is active by default is false and it's public because we need acess from other script
	public bool isCamera = false;

	// checking if the pause ui is active by bool
	private bool paused = false;
	// checkng if the decisions panel is active by bool
	private bool isOn = false;
	// checking if the ttuorial is active
	private bool tutOnce = true;
	//checking if the text is already typed
	private bool isText = true;
	private bool notYet1 = true;
	private bool notYet2 = true;
	private bool notYet3 = true;
	private bool notYet4 = true;
	private bool notYet5 = true;
	private bool notYet6 = true;
	private bool notYet7 = true;
	private bool notYet8 = true;
	private bool notYet9 = true;

	// getting different veriables and setting some of them to false
	private void Start()
	{
		aiTrigger = GameObject.Find("gidTrial").GetComponent<NpcAI>();
		text = GameObject.FindGameObjectWithTag("dialoguesText").GetComponent<Text>();

		if (File.Exists(Application.persistentDataPath + "/tutorialSaved.json") == true)
		{
			LoadTutorial();
		}

		if (!tutOnce)
		{
			notYet9 = false;
			ExitTutorial();
		}

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		player = gameObject.GetComponent<Player>();
		pauseUi.SetActive(false);
		decisionsTextPanel.SetActive(false);
		dialougePanel.SetActive(true);

		Tutorial();
	}

	private void Update()
	{
		//checking if escape is pressed everyframe so we can pause/unpause
		if (Input.GetButtonDown("Escape"))
		{
			paused = !paused;
			options.SetActive(false);
		}

		// if it's pause setting everyting to be active and player controller to be deactivated and time is set to 0 so noone can move
		if (paused)
		{
			isCamera = false;
			isOn = false;
			decisionsTextPanel.SetActive(false);
			dialougePanel.SetActive(false);
			ObjSelection.isPanelOn = false;
			ObjSelection.infoPanel.SetActive(false);
			ObjSelection.enabled = false;
			pauseUi.SetActive(true);
			player.enabled = false;
		}

		// reversing the upper function
		else if (!paused)
		{
			ObjSelection.enabled = true;
			pauseUi.SetActive(false);
			player.enabled = true;
		}

		// checking if decisionsPanel is active
		DecisionsTextPanel();
		// checking if the pcCamera is active
		PcCameraM();

		// deactivating the minimap if some those ui settings are true
		if (isCamera || isOn || paused || ObjSelection.isPanelOn || tutOnce)
		{
			player.enabled = false;
			cam.enabled = false;
			miniMap.SetActive(false);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			miniMap.SetActive(true);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			player.enabled = true;
			cam.enabled = true;
		}

		if(paused || isOn)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}


	}

	// the decisions panel method
	void DecisionsTextPanel()
	{
		// checking if the decisions panel is active
		if (isOn)
		{
			ObjSelection.enabled = false;
			decisionsTextPanel.SetActive(true);
			dialougePanel.SetActive(false);
		}

		// checking if the decisions panel is deactivated
		else if (!isOn)
		{
			ObjSelection.enabled = true;
			decisionsTextPanel.SetActive(false);
			dialougePanel.SetActive(true);
		}
	}

	// the pc camera method
	public void PcCameraM()
	{
		// checking if the camera bool is active
		if(isCamera)
		{
			pcCamera.depth = 0;
			ObjSelection.enabled = (false);
			playerCamera.enabled = false;
		}

		// checking if the camera bool is deactivated
		else if (!isCamera)
		{
			pcCamera.depth = -1;
			ObjSelection.enabled = (true);
			playerCamera.enabled = true;
		}

	}

	void Tutorial()
	{
		if(isText == true)
		{
			playerTutText.text = "Здравей! Добре дошъл в исторически музей - Кърджали! Аз съм твоя гид и сега ще ти обясня как да се предвижваш из музея!";
			isText = false;
		}
	}

	public void Tutorial2()
	{
		if (isText == false)
		{
			playerTutText.text = "";
			playerTutText.text = "За да започнеш да се движеш, изплозвай стрелките на клавиатурата или W, A, S, D! За да се огледаш наоколо използвай мишката!";
			notYet1 = false;
			buttonTutorial.SetActive(false);
			buttonTutorial2.SetActive(true);
		}
	}

	public void Tutorial3()
	{
		if (notYet1 == false)
		{
			playerTutText.text = "";
			playerTutText.text = "За да влезнеш или излезнеш от музея, трябва да се приближиш до входната врата и да натиснеш бутона Е!";
			notYet2 = false;
			buttonTutorial2.SetActive(false);
			buttonTutorial3.SetActive(true);
		}
	}

	public void Tutorial4()
	{
		if(notYet2 == false)
		{
			playerTutText.text = "";
			playerTutText.text = "По същият начи можеш да отиваш и на другите етажи на сградата, приближаваш се до стълбите и натискаш бутона Е!";
			notYet3 = false;
			buttonTutorial3.SetActive(false);
			buttonTutorial4.SetActive(true);
		}
	}

	public void Tutorial5()
	{
		if (notYet3 == false)
		{
			playerTutText.text = "";
			playerTutText.text = "След завършването на урока, в горният десен ъгъл ще видиш карта. Тя ще ти помогне да се ориентираш из етажите на музея!";
			notYet4 = false;
			buttonTutorial4.SetActive(false);
			buttonTutorial5.SetActive(true);
		}
	}

	public void Tutorial6()
	{
		if (notYet4 == false)
		{
			playerTutText.text = "";
			playerTutText.text = "Като влезнеш в сградата на музея ще ме видиш 'мен'- Гида да се разхождам из коридорите! Не бъди срамежлив/а ела да поговорим!";
			notYet5 = false;
			buttonTutorial5.SetActive(false);
			buttonTutorial6.SetActive(true);
		}
	}

	public void Tutorial7()
	{
		if (notYet5 == false)
		{
			playerTutText.text = "";
			playerTutText.text = "Като се приближиш до мен и натиснеш бутонът Е, ще можеш да ми задедеш въпроси, за да ти дам най-важната информация за експонатите, като ти разкажа тяхната история!";
			notYet6 = false;
			buttonTutorial6.SetActive(false);
			buttonTutorial7.SetActive(true);
		}
	}

	public void Tutorial8()
	{
		if (notYet6 == false)
		{
			playerTutText.text = "";
			playerTutText.text = "И още нещо! Вътре в музея ще видиш компютър! Като се приближиш до него и натиснеш бутона Е ще можеш да работиш на него!";
			notYet7 = false;
			buttonTutorial7.SetActive(false);
			buttonTutorial8.SetActive(true);
		}
	}

	public void Tutorial9()
	{
		if (notYet7 == false)
		{
			playerTutText.text = "";
			playerTutText.text = "Също така ще можеш да направиш тест, за да провериш какво си научил/a от разходката си или да влезнеш в сайта на музея! За да го затвориш натисни бутона ESC или използвай менюто за затваряне от долния ляв ъгъл!";
			notYet8 = false;
			buttonTutorial8.SetActive(false);
			buttonTutorial9.SetActive(true);
		}
	}

	public void Tutorial10()
	{
		if (notYet8 == false)
		{
			playerTutText.text = "";
			playerTutText.text = "Последно забравих да ти кажа, във всяка стая има табела с нейния номер, натисни върху нея с десен бутон на мишката, за да получиш подробна информация за дадената стая! Е вече нямам на какво да те науча, успех!";
			notYet9 = false;
			buttonTutorial9.SetActive(false);
			buttonTutorial10.SetActive(true);
		}
	}

	public void ExitTutorial()
	{
		if (notYet9 == false)
		{
			tutOnce = false;
			panelTutorial.SetActive(false);
			playerTutText.text = "";
			saveWithJ.tutOnceSave = tutOnce;
			string locationData = JsonUtility.ToJson(saveWithJ, true);
			File.WriteAllText(Application.persistentDataPath + "/tutorialSaved.json", locationData);
		}
	}

	private void LoadTutorial()
	{
		saveWithJ = JsonUtility.FromJson<TutorialSave>(File.ReadAllText(Application.persistentDataPath + "/tutorialSaved.json"));
		tutOnce = saveWithJ.tutOnceSave;
	}


	private void OnTriggerEnter(Collider co)
	{
		//checking if there is collision between the player and the npc if there is we're starting the turning animation
		if (co.tag == "leftTurnTag" || co.tag == "decisionsCollider")
		{
			text.text = "Как мога да помогна?";
			aiTrigger.animator.SetBool("isTurningLeft", true);
			aiTrigger.animator.SetBool("isIdle", false);
			aiTrigger.animator.SetBool("isWalking", false);
			aiTrigger.animator.SetBool("isTurningRight", false);
		}
		else if (co.tag == "rightTurnTag" || co.tag == "decisionsCollider")
		{
			text.text = "Как мога да помогна?";
			aiTrigger.animator.SetBool("isTurningRight", true);
			aiTrigger.animator.SetBool("isTurningLeft", false);
			aiTrigger.animator.SetBool("isIdle", false);
			aiTrigger.animator.SetBool("isWalking", false);
		}
	}

	private void OnTriggerStay(Collider co)
	{
		// checking if we hit the given tag and input the E button if everyting is true we're setting the decisions panel to be active
		if (Input.GetButtonDown("E") && co.tag == "decisionsCollider")
		{
			isOn = true;
		}

		// checking if we hit the given tag and input the E button if everyting is true we're setting the pcCamera to be focused
		if (Input.GetButtonDown("E") && co.tag == "pcCamera")
		{
			isCamera = true;
		}
		// checkinf if we're hitting the pcCamera collider and displaying the ask panel so we can know what button to press
		else if (co.tag == "pcCamera" && isCamera == false)
		{
			panelForPc.SetActive(true);
			textForPc.text = "Натиснете бутонът Е, за да започнете работа с компютърът";
		}
		else
		{
			panelForPc.SetActive(false);
		}

	}

	private void OnTriggerExit(Collider co)
	{
		textForPc.text = "";
		text.text = " ";
		isOn = false;
		isCamera = false;
		panelForPc.SetActive(false);
	}

	// next page to activate method
	public void ButtonsToDeactivate()
	{
		for (int i = 0; i < buttonsToDeactivate.Length; i++)
		{
			for (int b = 0; b < buttonsToActivate.Length; b++)
			{
				buttonsToDeactivate[i].SetActive(false);
				buttonsToActivate[b].SetActive(true);
			}
		}
	}

	// prev page to activate method 
	public void ButtonsToActivate()
	{
		for (int i = 0; i < buttonsToDeactivate.Length; i++)
		{
			for (int b = 0; b < buttonsToActivate.Length; b++)
			{
				buttonsToDeactivate[i].SetActive(true);
				buttonsToActivate[b].SetActive(false);
			}
		}
	}

	// the resume game button
	public void ResumeGame()
	{

		paused = !paused;

		if (!paused)
		{
			pauseUi.SetActive(false);
		}
	}

	// exit from decisions panel
	public void ResumeGameFromDecisions()
	{
		isOn = !isOn;

		if (!isOn)
		{
			decisionsTextPanel.SetActive(false);
			dialougePanel.SetActive(true);
			Time.timeScale = 1;
		}
	}

	// loading options
	public void LoadOptions()
	{
		options.SetActive(true);
	}

	// exit to main menu
	public void ExitLevel()
	{
		SceneManager.LoadScene("menu");
	}

	//reseting level
	public void ResetLevelFloor()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}