using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{

	public GameObject helpPanel;
	public GameObject startPanel;
	public Text text;

	private void Start()
	{
		helpPanel.SetActive(false);
	}

	//starting level1 aka courtyard
	public void StartLevel()
	{
		SceneManager.LoadScene("floor1LoadingScene");
	}

	//loading the options
	public void LoadOptionsMenu()
	{
		SceneManager.LoadScene("options");
	}

	//loading the helpmenu
	public void LoadHelpMenu()
	{
		helpPanel.SetActive(true);
		startPanel.SetActive(false);
	}

	public void LoadStartPanel()
	{
		text.text = "";
		helpPanel.SetActive(false);
		startPanel.SetActive(true);
	}

	public void Help1()
	{
		text.text = "";
		text.text = "За да се движиш из музея, използвай стрелките на клавиатурата или W, A, S, D. А, а за да се огледаш използвай мишката! Влизането в сградата и качването на другите етажи става с бутона Е!";
	}

	public void Help2()
	{
		text.text = "";
		text.text = "Като влезнеш в музея може да говориш с гида, като се приближиш до него и натиснеш бутона Е, след това с мишката избираш за коя стая искаш да получиш информация и гида ще отиде до нея, за да ти я каже!";
	}
	
	public void Help3()
	{
		text.text = "";
		text.text = "Като започнеш нова игра в горният десен ъгъл ще видиш карта! Тя ще ти помогне да се ориентираш из стаите на музея, за помощ сме сложили и номера на дадената стая отбелязани на картата!";
	}

	public void Help4()
	{
		text.text = "";
		text.text = "Като влезнеш вътре в музея ще видиш компютър! Можеш да работиш с него като се приближиш и натиснеш бутона Е! След това можеш да избереш да направиш тест, за да провериш своите знания или да влезнеш в сайта на музея! Във Всяка стая има и табела с надпис 'Стая №', като цъкнеш на нея с десен бутон на мишката ще получиш подробна информация за тази стая!";
	}

	//exiting the game
	public void ExitApp()
	{
		Application.Quit();
	}

}
