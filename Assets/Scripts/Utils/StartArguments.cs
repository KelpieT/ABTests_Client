using System;
using System.Linq;

public class StartArguments 
{
	private const string keyLevel = "-level";
	private const string keySessionsCount = "-sessionsCount";
	private const string keyTotalPlayTime = "-totalPlayTime";
	private const string keyPort = "-port";

	public UserParams GetUserParams()
	{
		string[] args = Environment.GetCommandLineArgs();
		if (!args.Contains(keyLevel) && !args.Contains(keySessionsCount) && !args.Contains(keyTotalPlayTime))
		{
			return null;
		}
		UserParams userParams = new UserParams();
		for (int i = 1; i < args.Length; i++)
		{
			string key = args[i - 1];
			string item = args[i];
			switch (key)
			{
				case keyLevel:
					userParams.level = int.Parse(item);
					break;
				case keySessionsCount:
					userParams.sessionsCount = int.Parse(item);
					break;
				case keyTotalPlayTime:
					userParams.totalPlayTime = int.Parse(item);
					break;
			}
		}
		return userParams;
	}

	public string GetPortArg()
	{
		return GetArg(keyPort);
	}

	private string GetArg(string keyArg)
	{
		string[] args = Environment.GetCommandLineArgs();
		// if (!args.Contains(keyArg))
		// {
		// 	throw new ArgumentException();
		// }
		for (int i = 1; i < args.Length; i++)
		{
			string key = args[i - 1];
			string item = args[i];
			if (key == keyArg)
			{
				return item;
			}
		}
		return null;
	}
}
