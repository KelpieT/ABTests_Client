using System;

[Serializable]
public class UserParams
{
	public int level;
	public int sessionsCount;
	public float totalPlayTime;
	public event Action OnChange;

	#region MethodsForDebug
	
	public void ChangeLevelValue(int value)
	{
		level += value;
		OnChange?.Invoke();
	}

	public void ChangeSessionsValue(int value)
	{
		sessionsCount += value;
		OnChange?.Invoke();
	}

	public void ChangeTotalPlayTimeValue(int value)
	{
		totalPlayTime += value;
		OnChange?.Invoke();
	}

	#endregion
}
