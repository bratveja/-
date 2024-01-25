using UnityEngine;
using UnityEngine.UI;

public class PcScreenManager : MonoBehaviour
{

	public GameObject optionsPanel; // the menu bar thingy
	public GameObject testpanel;
	public GameObject exitPcButton; // the exit button from the pc
	public Text text;
	public Text answer1;
	public Text answer2;
	public Text rightWrong;
	public int test;

	private PauseOptions pause; // reffernce to the pause options script

	private bool isExitMenu = true; // checking if we did exit the menu by the default is true
	private bool rightOrWrong = true;

	private void Start()
	{
		pause = GameObject.FindGameObjectWithTag("Player").GetComponent<PauseOptions>();
		test = Random.Range(1, 4);
	}

	public void OptionsPanel()
	{
		// showing the options menu
		if (isExitMenu)
		{
			optionsPanel.SetActive(true);
			exitPcButton.SetActive(true);
			isExitMenu = false;
		}

		// removing the options menu
		else
		{
			optionsPanel.SetActive(false);
			exitPcButton.SetActive(false);
			isExitMenu = true;
		}
	}

	public void TestPanel()
	{
		testpanel.SetActive(true);
		if(test == 1)
		{
			text.text = "От коя епоха датират най-ранните човешки следи в Източните Родопи?";
			answer1.text = "От Неолита!";
			answer2.text = "От Средновековието!";
		}
		else if(test == 2)
		{
			text.text = "Колко са на брои известните средновековни некрополи?";
			answer1.text = "Над 160!";
			answer2.text = "Над 200!";
		}
		else if(test == 3)
		{
			text.text = "В края на кой век Християнството е наложено като религия?";
			answer1.text = "В края на IV в.";
			answer2.text = "В края на VI в.";
		}
		else if(test == 4)
		{
			text.text = "Кога е било въстанието на бесите? ?";
			answer1.text = "От 15-11г. пр. Хр.";
			answer2.text = "От 21-28г.";
		}
	}

	public void TestPanelFloor2()
	{
		testpanel.SetActive(true);
		if (test == 1)
		{
			text.text = "От водите на кое море са били заляти Източните Родопи преди 30 млн. години?";
			answer1.text = "От Топло море!";
			answer2.text = "От Черно море!";
		}
		else if (test == 2)
		{
			text.text = "От коя река идва минерала пироморфит?";
			answer1.text = "р-к „Пчелояд”!";
			answer2.text = "р-к „Попско”!";
		}
		else if (test == 3)
		{
			text.text = "Обикновено с коя руда се бърка кварцът?";
			answer1.text = "Сфалерит!";
			answer2.text = "Калцит!";
		}
		else if (test == 4)
		{
			text.text = "Как се казва героят спасил Кърджали?";
			answer1.text = "Васил Делов!";
			answer2.text = "Васил Левски!";
		}
	}

	public void TestPanelFloor3()
	{
		testpanel.SetActive(true);
		if (test == 1)
		{
			text.text = "Кои са най-разпространените занаяти сред занаятчийско-търговски центрове?";
			answer1.text = "Медникарството, папукчийството и абаджийството!";
			answer2.text = "Обработка на глина, дърво, текстил! ";
		}
		else if (test == 2)
		{
			text.text = "Кои са били някои от домашните занаяти?";
			answer1.text = "Предене, тъкане, везане”!";
			answer2.text = "Обработка на глина, дърво, текстил!";
		}
		else if (test == 3)
		{
			text.text = "Коя е революционна система за източнородопското земеделие през XVI-XVII век?";
			answer1.text = "Проникването на царевицата, картофите и др.";
			answer2.text = "Отглеждането на лен и коноп!";
		}
		else if (test == 4)
		{
			text.text = "Какво е характерно за мъжката народна носия?";
			answer1.text = "Своеобразие!";
			answer2.text = "Разнообразие!";
		}
	}

	public void Right()
	{
		if(rightOrWrong)
		{
		rightOrWrong = false;
		rightWrong.text = "Поздравления! Ти позна въпроса, можеш да намериш своята награда в папката на играта!";
		}
	}

	public void Wrong()
	{
		if(rightOrWrong)
		{
		rightOrWrong = false;
		rightWrong.text = "За жалост ти сгреши! Може би следващия път?";
		}
	}

	public void ExitingTest()
	{
		testpanel.SetActive(false);
	}

	// exiting the pc screen and camera
	public void ExitPcScreen()
	{
		pause.isCamera = false;
	}

	// link to the museum site
	public void MuseumSite()
	{
		Application.OpenURL("http://www.rim-kardzhali.bg/");
	}

}