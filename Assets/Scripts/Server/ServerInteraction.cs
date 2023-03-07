using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ServerInteraction
{
	private const string url = "http://localhost:";
	private const string getNewUserID = "/GetNewUserID";
	private const string getAllTests = "/GetAllTests";
	private const string setTestsToUser = "/SetTestsToUser";
	private const string getServerParams = "/GetServerParams";
	private string port = "5227";
	private string serverUrl;

	public ServerInteraction()
	{
		serverUrl = url + port;
	}

	public ServerInteraction(string port)
	{
		this.port = port;
		serverUrl = url + port;
	}

	public async Task<Int64> GetNewUserIDAsync()
	{
		var getTask = GetAsync(serverUrl + getNewUserID);
		await getTask;
		return Int64.Parse(getTask.Result);
	}

	public async Task<AbTestsForSend> GetAllTestsAsync()
	{
		var getTask = GetAsync(serverUrl + getAllTests);
		await getTask;
		return JsonUtility.FromJson<AbTestsForSend>(getTask.Result);
	}

	public async Task SetTestsToUserAsync(Int64 userId, List<int> ids)
	{

		string url = serverUrl + setTestsToUser + "?id=" + userId.ToString() + "&list=" + string.Join(",", ids);
		var getTask = GetAsync(url);
		await getTask;
	}

	public async Task<ServerParams> GetServerParamsAsync(Int64 userID)
	{
		var getTask = GetAsync(serverUrl + getServerParams + "?id=" + userID.ToString());
		await getTask;
		return JsonUtility.FromJson<ServerParams>(getTask.Result);
	}

	private async Task<string> GetAsync(string url)
	{
		using UnityWebRequest webRequest = UnityWebRequest.Get(url);
		var asyncOperation = webRequest.SendWebRequest();
		while (!asyncOperation.isDone)
		{
			await Task.Yield();
		}
		if (webRequest.result == UnityWebRequest.Result.Success)
		{
			return webRequest.downloadHandler.text;
		}
		else
		{
			throw new Exception(webRequest.error);
		}
	}


}
