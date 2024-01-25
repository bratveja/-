using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nextFloor : MonoBehaviour
{

	public GameObject nextLevel; // the canvas Panel for the text
	public Slider loadingSlider; // loading slider obj
	public Text text; // the canvas panel text

	private CourtyardSavingLoading saveCourtyardLoc;
	private Floor1SavingLoading saveFloor1Loc;
	private Floor2SavingLoading saveFloor2Loc;

	private void Start()
	{
		saveFloor2Loc = GetComponent<Floor2SavingLoading>();
		saveCourtyardLoc = GetComponent<CourtyardSavingLoading>();
		saveFloor1Loc = GetComponent<Floor1SavingLoading>();
	}

	private void OnTriggerEnter(Collider co)
	{
		if (co.tag == "floor2 - floor1" || co.tag == "floor2 - floor3")
		{
			// telling to the panel to activate and display the given text
			nextLevel.SetActive(true);
			text.text = "Натиснете бутонът Е, за да преминете към следващият етаж";
		}
		else if(co.tag == "floor1 - floor2" || co.tag == "floor3 - floor2")
		{
			nextLevel.SetActive(true);
			text.text = "Натиснете бутонът Е, за да преминете към предишният етаж";
		}
		else if(co.tag == "courtYard - floor1")
		{
			nextLevel.SetActive(true);
			text.text = "За да влезете в музея, натиснете бутонът Е";
		}
		else if(co.tag == "floor1 - courtYard")
		{
			nextLevel.SetActive(true);
			text.text = "За да излезете от музея, натиснете бутонът Е";
		}
	}

	private IEnumerator OnTriggerStay(Collider co)
	{

		// if we hit this tag we go to floor 1 we're loading floor1
		if (co.tag == "courtYard - floor1")
		{
			// telling to the panel to activate and display the given text
			if (Input.GetKeyDown(KeyCode.E))
			{
				saveCourtyardLoc.SaveLocation();
				float fadeTime = GameObject.Find("fadeObj").GetComponent<FadeScript>().BeginFade(1);
				yield return new WaitForSeconds(fadeTime);
				SceneManager.LoadScene("courtYardLoadingScene");
			}
		}

		// if we hit this tag we go to floor 2 we're loading floor1
		if (co.tag == "floor2 - floor1")
		{
			// telling to the panel to activate and display the given text
			if (Input.GetKeyDown(KeyCode.E))
			{
				saveFloor1Loc.SaveLocation();
				float fadeTime = GameObject.Find("fadeObj").GetComponent<FadeScript>().BeginFade(1);
				yield return new WaitForSeconds(fadeTime);
				SceneManager.LoadScene("floor2LoadingScene");
			}
		}

		if(co.tag == "floor2 - floor3")
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				saveFloor2Loc.SaveLocation();
				float fadeTime = GameObject.Find("fadeObj").GetComponent<FadeScript>().BeginFade(1);
				yield return new WaitForSeconds(fadeTime);
				SceneManager.LoadScene("floor3LoadingScene");
			}
		}

		if (co.tag == "floor3 - floor2")
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				float fadeTime = GameObject.Find("fadeObj").GetComponent<FadeScript>().BeginFade(1);
				yield return new WaitForSeconds(fadeTime);
				SceneManager.LoadScene("floor2LoadingScene");
			}
		}

		// if we hit this tag we go to floor 1 we're loading floor1
		if (co.tag == "floor1 - floor2")
		{
			// telling to the panel to activate and display the given text
			if (Input.GetKeyDown(KeyCode.E))
			{
				float fadeTime = GameObject.Find("fadeObj").GetComponent<FadeScript>().BeginFade(1);
				yield return new WaitForSeconds(fadeTime);
				SceneManager.LoadScene("courtYardLoadingScene");
			}
		}

		// if we hit this tag we go to the courtyard we're loading the courtyard level
		if (co.tag == "floor1 - courtYard")
		{ 
			if (Input.GetKeyDown(KeyCode.E))
			{
				saveFloor1Loc.SaveLocation();
				float fadeTime = GameObject.Find("fadeObj").GetComponent<FadeScript>().BeginFade(1);
				yield return new WaitForSeconds(fadeTime);
				SceneManager.LoadScene("floor1LoadingScene");
			}
		}
	}

	private void OnTriggerExit(Collider co)
	{
		nextLevel.SetActive(false);
		text.text = "";
	}
}
