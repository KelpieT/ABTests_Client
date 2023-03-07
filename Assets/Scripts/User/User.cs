using System;
using UnityEngine;

public class User : MonoBehaviour
{
	private Int64 userID;
	private UserParams userParams;
	private ServerParams serverParams;

	public void Init(Int64 userID, UserParams userParams, ServerParams serverParams)
	{
		this.userID = userID;
		this.userParams = userParams;
		this.serverParams = serverParams;
	}

	public void UpdateUser(ServerParams serverParams)
	{
		this.serverParams = serverParams;
	}

	public void UpdateUser(UserParams userParams)
	{
		this.userParams = userParams;
	}

	public void UpdateUser(UserParams userParams, ServerParams serverParams)
	{
		this.userParams = userParams;
		this.serverParams = serverParams;
	}

	public Int64 GetUserID()
	{
		return userID;
	}

	#region MethodsForDebug

	public UserParams GetUserParams()
	{
		return userParams;
	}

	public ServerParams GetServerParams()
	{
		return serverParams;
	}

	#endregion
}
