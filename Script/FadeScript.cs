using UnityEngine;

public class FadeScript : MonoBehaviour
{

	public Texture2D fadeOut; // the texture that will overlay the screen
	public float fadeSPeed = 0.0f; //the fading speed

	private int drawDepth = -99; // the texture depth in hierarchy
	private float fadeDir = -1; // the fade direction
	private float alpha = 1.0f;  // the texture alpha will be between 0 and -1

	private void OnGUI()
	{
		// fade out/in the alpha value
		alpha += fadeDir * fadeSPeed * Time.deltaTime;

		// clamp value between 0 and 1
		alpha = Mathf.Clamp01(alpha);

		// set color of the gui (texture)
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha); //setting the alpha
		GUI.depth = drawDepth; // the gui depth
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOut); // draw the texture to the entire screen
	}

	// the direction to fade 
	public float BeginFade (float direction)
	{
		fadeDir = direction;
		return (fadeSPeed); // return the fadeSpeed
	}

	// starting the fade when the level start
	private void OnLevelWasLoaded(int level)
	{
		BeginFade(-1);
	}
}
