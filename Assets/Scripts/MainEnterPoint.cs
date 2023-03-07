using System;
using System.Collections.Generic;
using UnityEngine;

public class MainEnterPoint : MonoBehaviour
{
	[SerializeField] private User user;
	[SerializeField] private DebugUI debugUI;
	[SerializeField] private MainAbTests mainAbTests;

	private ServerInteraction serverInteraction;
	private UserParams userParams;

	private async void Awake()
	{
		debugUI.SetLoadingScreen(true);
		InitStartParams();

		userParams.OnChange += mainAbTests.CheckTestsCondition;

		var taskID = serverInteraction.GetNewUserIDAsync();
		var taskGetAllTests = serverInteraction.GetAllTestsAsync();
		await taskID;
		Int64 userID = taskID.Result;
		mainAbTests.AddedNewTest += UpdateTests;

		await taskGetAllTests;
		mainAbTests.Init(taskGetAllTests.Result, userParams);
		List<int> testsIds = mainAbTests.GetParticipatingTest();

		var taskSetTestsToUser = serverInteraction.SetTestsToUserAsync(userID, testsIds);
		await taskSetTestsToUser;

		var taskGetServerParams = serverInteraction.GetServerParamsAsync(userID);
		await taskGetServerParams;

		user.Init(userID, userParams, taskGetServerParams.Result);
		debugUI.Init(user, mainAbTests);

		Debug.Log("Inits done");
		debugUI.SetLoadingScreen(false);
	}

	private void InitStartParams()
	{
		StartArguments startArguments = new StartArguments();

		var paramsFromArgs = startArguments.GetUserParams();
		var port = startArguments.GetPortArg();

		serverInteraction = port != null ? new ServerInteraction(port) : new ServerInteraction();
		userParams = paramsFromArgs != null ? paramsFromArgs : new UserParams();
	}

	private async void UpdateTests()
	{
		await serverInteraction.SetTestsToUserAsync(user.GetUserID(), mainAbTests.GetParticipatingTest());
		var taskGetServerParams = serverInteraction.GetServerParamsAsync(user.GetUserID());
		await taskGetServerParams;
		user.UpdateUser(taskGetServerParams.Result);
		debugUI.UpdateUI();
	}

	private void OnDestroy()
	{
		userParams.OnChange -= mainAbTests.CheckTestsCondition;
		mainAbTests.AddedNewTest -= UpdateTests;
	}
}
