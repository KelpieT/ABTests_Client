using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugUIValuePanel : MonoBehaviour
{
	public event Action<int> OnValueChange;
	public Func<string> getNameValueFunc;
	public Func<string> getValueFunc;

	[SerializeField] private Button increase;
	[SerializeField] private Button decrease;
	[SerializeField] private Text nameValue;
	[SerializeField] private Text value;

	private void OnEnable()
	{
		increase.onClick.AddListener(IncreaseValue);
		decrease.onClick.AddListener(DecreaseValue);
	}

	private void OnDisable()
	{
		increase.onClick.RemoveListener(IncreaseValue);
		decrease.onClick.RemoveListener(DecreaseValue);
	}
	
	public void Init()
	{
		SetNameValueText();
		SetValueText();
	}

	private void SetNameValueText()
	{
		if (getNameValueFunc != null)
		{
			nameValue.text = getNameValueFunc.Invoke();
		}
	}

	private void SetValueText()
	{
		if (getValueFunc != null)
		{
			value.text = getValueFunc.Invoke();
		}
	}
	private void IncreaseValue()
	{
		OnValueChange?.Invoke(1);
		SetValueText();
	}
	private void DecreaseValue()
	{
		OnValueChange?.Invoke(-1);
		SetValueText();
	}

}
