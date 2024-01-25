using System.IO;
using UnityEngine;

[RequireComponent(typeof(floor1Location))]
public class Floor1SavingLoading : MonoBehaviour
{

	private float x;
	private float y;
	private float z;

	public floor1Location location;

	private void Start()
	{ 
		if(File.Exists(Application.persistentDataPath + "/floor1Location.json") == true)
		{
		LoadLocation();
		}
	}

	//private void Start()
	//{
	//	x = transform.position.x;
	//	y = transform.position.y;
	//	z = transform.position.z;
	//}

	//private void Update()
	//{
	//	if(Input.GetKeyDown(KeyCode.F2))
	//	{
	//		SaveLocation();
	//	}

	//	if(Input.GetKeyDown(KeyCode.F3))
	//	{
	//		LoadLocation();
	//	}
	//}

	private void FixedUpdate()
	{
		GetLocation();
	}

	private void GetLocation()
	{

		Vector3 _location = new Vector3(x, y, z);

		_location.x = transform.position.x;
		_location.y = transform.position.y;
		_location.z = transform.position.z;

		location.x = _location.x;
		location.y = _location.y;
		location.z = _location.z;
	}

	public void SaveLocation()
	{
		string locationData = JsonUtility.ToJson(location, true);
		File.WriteAllText(Application.persistentDataPath + "/floor1Location.json", locationData);
	}

	private void LoadLocation()
	{
		location = JsonUtility.FromJson<floor1Location>(File.ReadAllText(Application.persistentDataPath + "/floor1Location.json"));


		transform.position = new Vector3(location.x, location.y, location.z);
	}

}
