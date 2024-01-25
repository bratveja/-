using UnityEngine;

// the inforamtion panel class so we can have diffenet items
[CreateAssetMenu(fileName = "NewInfoPanel", menuName = "PanelsForInforamtions")]
public class InfoPanelText : ScriptableObject
{
	public new string name;
	public string description;
	public Sprite img;
}
