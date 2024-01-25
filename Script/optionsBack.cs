using UnityEngine;

public class optionsBack : MonoBehaviour
{

    public GameObject pauseUi;

    GameObject active;
    optionsSaved _active;

    public bool paused = false;

    void Start()
    {
        active = GameObject.Find("optionsSaved");
        _active = active.GetComponent<optionsSaved>();
        pauseUi.SetActive(false);
    }

    void Update()
    {
        if (_active.paused == false)
        {
            if (Input.GetButtonDown("Pause"))
            {
                paused = !paused;
            }

            if (paused)
            {
                Cursor.visible = true;
                pauseUi.SetActive(true);
                //Time.timeScale = 0;
            }
            

            if (!paused)
            {
                pauseUi.SetActive(false);
                //Time.timeScale = 1;
            }
            
        }
    }

    public void Paused()
    {

        paused = !paused;

        if (paused)
        {
            Cursor.visible = true;
            pauseUi.SetActive(true);
            //Time.timeScale = 0;

        }

        if (!paused)
        {
            pauseUi.SetActive(false);
            //Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        paused = false;
    }

}
