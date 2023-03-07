using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
	[SerializeField] private DebugUIValuePanel levelPanel;
	[SerializeField] private DebugUIValuePanel sessionPanel;
	[SerializeField] private DebugUIValuePanel playTimePanel;
	[SerializeField] private Text tests;
	[SerializeField] private Text serverParamsText;
	[SerializeField] private GameObject loadingScreen;

	private User user;
	private UserParams userParams;
	private ServerParams serverParams;
	MainAbTests abTests;

	public void Init(User user, MainAbTests abTests)
	{
		this.user = user;
		this.abTests = abTests;
		userParams = user.GetUserParams();
		serverParams = user.GetServerParams();
		AddListeners();
		UpdateUI();
	}
	
	public void UpdateUI()
	{
		ShowServerParams();
		SetPanels();
		ShowTests();
	}

	private void OnDestroy()
	{
		RemoveListeners();
	}

	private void ShowTests()
	{
		string text = "Tests:\n";
		foreach (var item in abTests.participatingTest)
		{
			text += "ID -" + item.id.ToString() + "\n";
		}
		tests.text = text;
	}

	private void SetPanels()
	{
		levelPanel.Init();
		sessionPanel.Init();
		playTimePanel.Init();
	}

	public void SetLoadingScreen(bool isLoading)
	{
		loadingScreen.SetActive(isLoading);
	}

	private void ShowServerParams()
	{
		string text = "";
		var fieldInfos = serverParams.GetType().GetFields();
		foreach (var item in fieldInfos)
		{
			text += item.Name + " - ";
			text += item.GetValue(serverParams);
			text += "\n";
		}
		serverParamsText.text = text;
	}

	private void AddListeners()
	{
		levelPanel.OnValueChange += userParams.ChangeLevelValue;
		levelPanel.getNameValueFunc = () => nameof(userParams.level);
		levelPanel.getValueFunc = () => userParams.level.ToString();

		sessionPanel.OnValueChange += userParams.ChangeSessionsValue;
		sessionPanel.getNameValueFunc = () => nameof(userParams.sessionsCount);
		sessionPanel.getValueFunc = () => userParams.sessionsCount.ToString();

		playTimePanel.OnValueChange += userParams.ChangeTotalPlayTimeValue;
		playTimePanel.getNameValueFunc = () => nameof(userParams.totalPlayTime);
		playTimePanel.getValueFunc = () => userParams.totalPlayTime.ToString();
	}

	private void RemoveListeners()
	{
		levelPanel.OnValueChange -= userParams.ChangeLevelValue;
		levelPanel.getNameValueFunc = null;
		levelPanel.getValueFunc = null;

		sessionPanel.OnValueChange -= userParams.ChangeSessionsValue;
		sessionPanel.getNameValueFunc = null;
		sessionPanel.getValueFunc = null;

		playTimePanel.OnValueChange -= userParams.ChangeTotalPlayTimeValue;
		playTimePanel.getNameValueFunc = null;
		playTimePanel.getValueFunc = null;
	}
}
