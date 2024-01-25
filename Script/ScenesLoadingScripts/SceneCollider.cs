using UnityEngine;

public class SceneCollider : MonoBehaviour
{

	bool hit1 = true;
	bool hit2 = true;
	bool hit3 = true;

	private void OnTriggerEnter(Collider co)
	{
		if(co.tag == "scene1and2" && hit1 == true)
		{
			hit1 = false;
			ScenesLoadingSequence.Instance.LoadScene("Scene2");
			ScenesLoadingSequence.Instance.LoadScene("Scene3");
		}

		if(co.tag == "scene4" && hit2 == true)
		{
			hit2 = false;
			ScenesLoadingSequence.Instance.LoadScene("Scene4");
		}

		if(co.tag == "scene5" && hit3 == true)
		{
			hit3 = false;
			ScenesLoadingSequence.Instance.LoadScene("Scene5");
		}
	}
}
