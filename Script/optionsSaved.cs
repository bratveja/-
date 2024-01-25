using UnityEngine;


public class optionsSaved : MonoBehaviour
{

    public GameObject pauseUi;

    GameObject active;
    optionsBack _active;

    public bool paused = false;

    void Start()
    {
        active = GameObject.Find("Paused");
        _active = active.GetComponent<optionsBack>();
        pauseUi.SetActive(false);
    }

    void Update()
    {
        if (_active.paused == true)
        {
            paused = false;
        }
        else if (_active.paused == false)
        {
            if (Input.GetButtonDown("SaveButton"))
            {
                paused = !paused;
            }

            if (paused)
            {
                pauseUi.SetActive(true);
                Time.timeScale = 0;
            }

            if (!paused)
            {
                pauseUi.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
    
    public void PausedUI()
    {
       
            paused = !paused;

        if (paused)
        {

            pauseUi.SetActive(true);
            Time.timeScale = 0;

        }

        if (!paused)
        {
            pauseUi.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
