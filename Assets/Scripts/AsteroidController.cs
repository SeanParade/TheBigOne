using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

	[SerializeField]
	public float speed;	
	[SerializeField]
	private float topY;
	[SerializeField]
	private float bottomY;
	[SerializeField]
	private float leftX;
	[SerializeField]
	private float rightX;
	[SerializeField]
	public float RotationSpeed;

	private Transform _transform;
	private Vector2 _currentPos;
	private Vector3 _currentRot;


	void Start () {
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;
	}
	
	void Update () {
		// Larger the mass, the slower the player is
		float _effectiveSpeed = speed - Player.Instance.Mass;

		_currentPos = _transform.position;
		

		float userIOH = Input.GetAxis ("Horizontal");
		float userIOV = Input.GetAxis ("Vertical");

		if (userIOH < 0) {	
			//left arrow = move left
			_currentPos -= new Vector2 (_effectiveSpeed, 0);
		}
		if (userIOH > 0) {	
			//right arrow = move right
			_currentPos += new Vector2 (_effectiveSpeed, 0);
		}

		if (userIOV < 0) {	
			//down arrow = move down
			_currentPos -= new Vector2 (0, _effectiveSpeed);
		}
		if (userIOV > 0) {	
			//up arrow = move up
			_currentPos += new Vector2 (0, _effectiveSpeed);
		}

		//Rotation; Slowed by Mass.
		_transform.Rotate(new Vector3(0,0,1) * ((RotationSpeed / (Player.Instance.Mass * Player.Instance.Mass)) * Time.deltaTime));

		CheckBounds ();
		_transform.position = _currentPos;
	}

	private void CheckBounds(){
		if (_currentPos.y < bottomY) {
			_currentPos.y = bottomY;
		}

		if (_currentPos.y > topY) {
			_currentPos.y = topY;
		}		

		if (_currentPos.x < leftX) {
			_currentPos.x = leftX;
		}
		if (_currentPos.x > rightX) {
			_currentPos.x = rightX;
		}
	}

}


