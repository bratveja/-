using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(GameSettings))]
public class Options : MonoBehaviour
{

	public PauseOptions pause; // refference to the pause menu so we can use it's variables
	public AudioMixer masterVolumeController; // getting the volume controller
	public GameObject exitUi; // asking the player if he want to exit panel
	public Slider volumeSlider; // volume controller for playerprefs
	public Slider sensitivitySlider; // sensitivity controller
	public Dropdown resDropDown; // the resolution menu
	public Dropdown quality; // quality dropdown for playerprefs
	public Toggle invertMouseToggle; // the invert mouse on/off switch
	public Toggle muteAudio; // muting the audio
	public Toggle fullscreen; // setting the fullsreen
	public Text mouseSensText; // the mouse sensitivity text
	public GameSettings gameSettings; // settings varialbe for axessing the values and saving them
	public CameraController cam; // the camera controller variable for getting the mouse settings

	private bool paused = false; // checking it we're paused

	Resolution[] res; // resolutions array

	private void Start()
	{

		// setting the res variable to be the screen resolution
		res = Screen.resolutions;

		// celaring the drodown menu for the resolutions
		resDropDown.ClearOptions();

		//creating list with options for the resolution
		List<string> options = new List<string>();

		// setting the screen resolution
		int currentRes = 0;
		for (int i = 0; i < res.Length; i++)
		{
			string option = res[i].width + " x " + res[i].height;
			options.Add(option);

			if (res[i].width == Screen.currentResolution.width && res[i].height == Screen.currentResolution.height)
			{
				currentRes = i;
			}
		}

		//adding opitons refreshing them and setting values
		resDropDown.AddOptions(options);
		resDropDown.value = currentRes;
		resDropDown.RefreshShownValue();

		// refreshing the quality dropdown menu 
		quality.RefreshShownValue();

		// rounding the sensitivity value
		sensitivitySlider.value = Mathf.Round(sensitivitySlider.value * 100f) / 100f;

		//checking if the gamesettings file is created and loading the settings if it is
		if (File.Exists(Application.persistentDataPath + "/gamesettings.json") == true)
		{
			LoadSettings();
		}
	}

	private void Update()
	{
		// rounding the sens text value to one sign after the float value
		mouseSensText.text = sensitivitySlider.value.ToString("F1");
	}

	//setting the res method
	public void SetResolution(int resIndex)
	{
		Resolution _res = res[resIndex];
		Screen.SetResolution(_res.width, _res.height, Screen.fullScreen);
		gameSettings.resDropDown = resIndex;
	}

	// the volume controller higher or lower amount 
	public void SetVolume()
	{
		masterVolumeController.SetFloat("masterVolumeController", volumeSlider.value);
		gameSettings.volumeSlider = volumeSlider.value;
	}

	// quality settings
	public void SetQuality()
	{
		QualitySettings.SetQualityLevel(quality.value);
		gameSettings.quality = quality.value;
	}

	//fullscreen button
	public void SetFullScreen(bool isFullscreen)
	{
		fullscreen.isOn = isFullscreen;
		Screen.fullScreen = isFullscreen;
		gameSettings.fullScreen = fullscreen.isOn;
	}

	public void InvertMouse()
	{
		cam.isInverted = invertMouseToggle.isOn;
		gameSettings.invertMouseToggle = invertMouseToggle.isOn;
	}

	public void MouseSpeed()
	{
		cam.mouseSensitivity = sensitivitySlider.value;
		gameSettings.sensitivitySlider = sensitivitySlider.value;
	}

	// muting the audio
	public void SetVolumeToggle(bool isMuted)
	{
		muteAudio.isOn = isMuted;
		if (isMuted)
		{
			masterVolumeController.SetFloat("masterVolumeController", 0);
		}
		else
		{
			masterVolumeController.SetFloat("masterVolumeController", -80);
		}
		gameSettings.muteSound = muteAudio.isOn;
	}

	// the ask panel if we want to exit the options
	public void ExitOptionsAsk()
	{
		paused = !paused;

		if (paused)
		{
			Cursor.visible = true;
			exitUi.SetActive(true);
		}

		if (!paused)
		{
			exitUi.SetActive(false);
		}
	}

	//resuming the options
	public void ResumeExitMenu()
	{
		paused = !paused;

		if(!paused)
		{
			exitUi.SetActive(false);
			Time.timeScale = 1;
		}
	}

	//exitting the options while in playmode
	public void ExitOptionsFromPlayMode()
	{
		pause.options.SetActive(false);
		exitUi.SetActive(false);
		SaveSettings();
	}

	//saving the options settings in json file the path is in appdata/gamename
	public void SaveSettings()
	{
		string jsonData = JsonUtility.ToJson(gameSettings, true);
		File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
	}
	
	//loading the settings
	public void LoadSettings()
	{

		gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

		volumeSlider.value = gameSettings.volumeSlider;
		resDropDown.value = gameSettings.resDropDown;
		quality.value = gameSettings.quality;
		sensitivitySlider.value = gameSettings.sensitivitySlider;
		invertMouseToggle.isOn = gameSettings.invertMouseToggle;
		muteAudio.isOn = gameSettings.muteSound;

	}

	//exiting the options
	public void ExitOptions()
	{
		SceneManager.LoadScene("menu");
		SaveSettings();
	}

}