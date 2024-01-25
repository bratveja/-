using UnityEngine;
using UnityEngine.UI;

public class ObjectSelection : MonoBehaviour
{
	public GameObject infoPanel; // reference to the info panel
	public Image img; // the sprite to be displayed when the panel is active
	public Text text; // the text to be displayed when the panel is active
	public Camera mainCam; // the camera for the ray to be casted from 

	[SerializeField] private InfoPanelText[] infoPanelItem; // reference to the item script
	[SerializeField] private GameObject[] cases; // reference to all of the objects to be albe to be selected
	[SerializeField] private Shader shader; // reference to the reworked default shared
	[SerializeField] private Shader defaultShader; // reference to the default shader

	public bool isPanelOn = false;

	private void Start()
	{
		cases = GameObject.FindGameObjectsWithTag("cases");
		shader = Shader.Find("Custom/Reworked");
		defaultShader = Shader.Find("Standard");
	}

	private void Update()
	{

		// setting ray to be casted from the middle of the screen
		Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); //setting up ray to be cast at the center of the screen
		float rayLength = 5f;

		//casting ray from the middle of the screen
		Ray ray = mainCam.ViewportPointToRay(rayOrigin);

		// checking if the casted ray hit something
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, rayLength))
		{
				// going through all of the cases objsects
				for (int i = 0; i < cases.Length; i++)
				{
					//Highligthing single object and checking for the object name
					if (hit.collider.name == cases[i].name)
					{
					//using the custom made shader
					Renderer[] rs = cases[i].GetComponents<Renderer>();
					foreach (Renderer r in rs)
					{
						Shader s = r.material.shader;
						s = shader;
						r.material.shader = s;
					}

					// checking if the left mouse buttons is presed
					if (Input.GetButtonDown("Fire1"))
						{
							// going through all items
							for (int b = 0; b < infoPanelItem.Length; b++)
							{
								// checking if the obj name is the same as the item name if it is to do everyting else
								if (hit.collider.name == infoPanelItem[b].name)
								{
									isPanelOn = true;
									if (isPanelOn)
									{
										text.text = infoPanelItem[b].description;
										img.sprite = infoPanelItem[b].img;
										infoPanel.SetActive(true);
									}
								}
							}
						}
					}
				//returning the default shader no highlighting
				else
				{
					Renderer[] rs = cases[i].GetComponents<Renderer>();
					foreach (Renderer r in rs)
					{
						Shader s = r.material.shader;
						s = defaultShader;
							r.material.shader = s;
					}
				}
			}
			}
		else
		{
			isPanelOn = false;
			infoPanel.SetActive(false);
		}
	}

	// exit button
	public void ExitInfoPanel()
	{
		isPanelOn = false;
		infoPanel.SetActive(false);
	}
}