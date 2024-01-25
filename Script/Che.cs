using UnityEngine;

public class Che : MonoBehaviour
{

	public GameObject[] chessFigures; // chess figures array
	public GameObject textForPressingButton; // the text mesht to appear above the object
	public Camera cam; // the camera for the figures to be centered at

	private void Update()
	{

		Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
		float rayLength = 5f;

		Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, rayLength))
		{
			for (int i = 0; i < chessFigures.Length; i++)
			{
				if (hit.collider.name == chessFigures[i].name)
				{
					textForPressingButton.SetActive(true);
					if (Input.GetMouseButtonDown(1))
					{
						chessFigures[i].GetComponent<Rigidbody>().useGravity = false;
						chessFigures[i].GetComponent<Rigidbody>().isKinematic = true;
						chessFigures[i].transform.parent = gameObject.transform;
					}
					else if (Input.GetMouseButtonUp(1))
					{
						chessFigures[i].GetComponent<Rigidbody>().useGravity = true;
						chessFigures[i].GetComponent<Rigidbody>().isKinematic = false;
						chessFigures[i].transform.parent = null;
					}
				}
			}
		}
		else textForPressingButton.SetActive(false);

	}
}