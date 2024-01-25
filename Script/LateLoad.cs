using UnityEngine;

public class LateLoad : MonoBehaviour
{

	public GameObject[] objectsToLoad;

	private void OnTriggerEnter(Collider co)
	{
		if(co.tag == "Player")
		{
			for (int i = 0; i < objectsToLoad.Length; i++)
			{
				objectsToLoad[i].SetActive(true);
			}
		}

	}


}
