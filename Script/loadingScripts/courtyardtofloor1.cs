using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class courtyardtofloor1 : MonoBehaviour
{
	public Slider loadingSlider;

	private void Start()
	{
		Floor1LoadingScene();
	}

	// loading progress scene from floor1 to courtyard
	private void Floor1LoadingScene()
	{
		StartCoroutine(LoadAsync("floor1"));
	}

	private IEnumerator LoadAsync(string sceneName)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

		while (!operation.isDone)
		{

			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			loadingSlider.value = progress;

			yield return null;
		}

	}
}
