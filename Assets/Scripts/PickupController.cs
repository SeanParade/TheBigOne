using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

	//Public variables
	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float startY;
	[SerializeField]
	private float endY;
	[SerializeField]
	private float startX;
	[SerializeField]
	private float endX;


	//Position variables
	private Transform _transform;
	private Vector2 _currentPos;




	void Start () {
		_transform = gameObject.GetComponent<Transform> ();
		Reset ();
	}
		
	void Update () {
		_currentPos = _transform.position;
		_currentPos -= new Vector2 (speed, 0);

		if (_currentPos.x < endX) {
			Reset ();
		}
		_transform.position = _currentPos;

	}


	public void Reset(){
		//Reset Position, multiply random intervals by player mass
		// more mass = longer waits in between pickups
		float y = Random.Range (startY, endY);
		float x = Random.Range (startX * Player.Instance.Mass , 4000 * Player.Instance.Mass);

		_currentPos = new Vector2 (x, y);
		_transform.position = _currentPos;
	}
}
