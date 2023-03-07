using System;
using System.Globalization;
using DynamicExpresso;

[Serializable]
public class AbTest
{
	public int id;
	public string name;
	public string startCondition;
	public string endCondition;
	public string startDate;
	public string endDate;
	private bool wasStarted = false;
	private DateTime dateStart;
	private DateTime dateEnd;


	public bool IsStartCondition(UserParams userParams)
	{
		return CheckCondition(userParams, startCondition);
	}

	public bool IsEndCondition(UserParams userParams)
	{
		return CheckCondition(userParams, endCondition);
	}

	private bool CheckCondition(UserParams userParams, string condition)
	{
		var interpreter = new Interpreter().SetVariable("userParams", userParams);
		Lambda parsedExpression = interpreter.Parse(condition);
		var result = parsedExpression.Invoke();
		return (bool)result;
	}

	public DateTime GetStartDate()
	{
		if (dateStart == default)
		{
			dateStart = DateTime.Parse(startDate, CultureInfo.InvariantCulture);
		}
		return dateStart;
	}

	public DateTime GetEndDate()
	{

		if (dateEnd == default)
		{
			dateEnd = DateTime.Parse(endDate, CultureInfo.InvariantCulture);
		}
		return dateEnd;
	}



}
