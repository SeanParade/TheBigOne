using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleController : MonoBehaviour {

	[SerializeField]
	float minYSpeed = -2f;
	[SerializeField]
	float maxYSpeed = 2f;
	[SerializeField]
	float minXSpeed = 5f;
	[SerializeField]
	float maxXSpeed = 10f;
	[SerializeField]
	float RotationSpeed = 100f;

	private Transform _transform;
	private Vector2 _currentSpeed;
	private Vector2 _currentPosition;

	void Start () {
		_transform = gameObject.GetComponent<Transform> ();
		Reset ();
	}

	public void Reset(){

		float xSpeed = Random.Range (minXSpeed, maxXSpeed);
		float ySpeed = Random.Range (minYSpeed, maxYSpeed);

		_currentSpeed = new Vector2 (xSpeed, ySpeed);

		float y = Random.Range (-400, 400); 
		_transform.position = 
			new Vector2 (750 + Random.Range (0, 100), y);
	
	}
	
	void Update () {
		_currentPosition = _transform.position;
		_currentPosition -= _currentSpeed;
		_transform.position = _currentPosition;

		if (_currentPosition.x <= -750) {
			// Award points for dodging missle and reset
			Player.Instance.Score += 50;
			Reset ();
		}
	}
}
