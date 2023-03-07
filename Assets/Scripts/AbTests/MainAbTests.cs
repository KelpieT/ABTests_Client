using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainAbTests : MonoBehaviour
{
	public List<AbTest> participatingTest = new List<AbTest>();
	private List<AbTest> incomingTests = new List<AbTest>();
	private UserParams userParams;
	public event Action AddedNewTest;
	public event Action EndedTest;

	public void Init(AbTestsForSend abTestsForSend, UserParams userParams)
	{
		this.userParams = userParams;
		var allTests = abTestsForSend.tests;
		InitAllLists(userParams, allTests);
	}


	private void InitAllLists(UserParams userParams, List<AbTest> allTests)
	{
		foreach (var item in allTests)
		{
			if (item.GetEndDate() >= System.DateTime.UtcNow && !item.IsEndCondition(userParams))
			{
				if (item.GetStartDate() < System.DateTime.UtcNow && item.IsStartCondition(userParams))
				{
					if (!participatingTest.Any(x => item.id == x.id))
					{
						participatingTest.Add(item);
					}
				}
				else
				{
					if (!incomingTests.Any(x => item.id == x.id))
					{
						incomingTests.Add(item);
					}
				}
			}
		}
	}

	public void CheckTestsCondition()
	{
		CheckEndedTests();
		CheckStartedTests();
	}

	private void CheckEndedTests()
	{
		List<AbTest> endedTests = new List<AbTest>();
		foreach (var item in participatingTest)
		{
			if (!item.IsEndCondition(userParams))
			{
				continue;
			}
			endedTests.Add(item);
		}
		foreach (var item in endedTests)
		{
			participatingTest.Remove(item);
		}
		
		EndedTest?.Invoke();
	}

	private void CheckStartedTests()
	{
		List<AbTest> started = new List<AbTest>();
		foreach (var item in incomingTests)
		{
			if (!item.IsStartCondition(userParams) || item.GetStartDate() > System.DateTime.UtcNow)
			{
				continue;
			}
			started.Add(item);
			if (!participatingTest.Any(x => item.id == x.id))
			{
				participatingTest.Add(item);
			}
		}
		if (started.Count > 0)
		{
			AddedNewTest?.Invoke();
		}
		foreach (var item in started)
		{
			incomingTests.Remove(item);
		}
	}

	public List<int> GetParticipatingTest()
	{
		List<int> ids = new List<int>();
		ids = participatingTest.Select(x => x.id).ToList();
		return ids;
	}

}
