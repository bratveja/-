using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesLoadingSequence : MonoBehaviour
{

	public static ScenesLoadingSequence Instance { set; get; }

	private void Awake()
	{
		Instance = this;

		LoadScene("scene1Floor");
		StartCoroutine("WaitBeforeLoad");
	}


	public void LoadScene(string name)
	{
		if (!SceneManager.GetSceneByName(name).isLoaded) ;
		{
			SceneManager.LoadScene(name, LoadSceneMode.Additive);
		}
	}

	public void UnloadSceneManager(string name)
	{
		if(SceneManager.GetSceneByName(name).isLoaded)
		{
			SceneManager.UnloadSceneAsync(name);
		}
	}

	private IEnumerator WaitBeforeLoad()
	{
		yield return new WaitForSeconds(0.15f);
		LoadScene("scene1Doors");

		yield return new WaitForSeconds(0.15f);
		LoadScene("Scene1coridorObjs");

		yield return new WaitForSeconds(0.15f);
		LoadScene("Scene1Lights");

		yield return new WaitForSeconds(0.15f);
		LoadScene("Scene1Reflections");

		yield return new WaitForSeconds(0.15f);
		LoadScene("Scene1Canvas");
	}

}
