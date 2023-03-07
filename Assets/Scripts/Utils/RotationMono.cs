using UnityEngine;

public class RotationMono : MonoBehaviour
{
	[SerializeField] private Vector3 speedRotation;
	[SerializeField] private Transform rotateObject;

	private void Update() => Rotate();

	private void Rotate()
	{
		if (rotateObject == null)
		{
			return;
		}
		rotateObject.rotation = Quaternion.Euler(rotateObject.rotation.eulerAngles + speedRotation * Time.deltaTime);
	}
}
