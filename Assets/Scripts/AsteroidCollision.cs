using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour {

	[SerializeField]
	GameController gctl;

	private AudioSource _thud;


	void Start (){
		_thud = gameObject.GetComponent<AudioSource> ();
	}


	public void OnTriggerEnter2D(Collider2D other){
		// play thud
		if (_thud != null) {
			_thud.Play ();
		}
		// play colliding object's sound
		if (other.gameObject.GetComponent<AudioSource> () != null) {
			other.gameObject.GetComponent<AudioSource> ().Play ();
		}

		// pickup logic
		if (other.gameObject.tag.Equals ("masspickup")) {
			Debug.Log ("Mass collected\nCurrent Mass: " + Player.Instance.Mass + "\n");

			if (Player.Instance.Mass < 4) {
				//Grow and update Mass
				other.gameObject.GetComponent<PickupController> ().Reset ();
				// Increase size of player and pickup
				grow (this.gameObject);
				grow (other.gameObject);
				Player.Instance.Mass++;

			} else {
				//if 5th pickup: Increase size of play and destroy pickups
				grow (this.gameObject);
				Destroy (other.gameObject);
				Player.Instance.Mass++;
			}
		}

		// enemy logic
		else if (other.gameObject.tag.Equals ("enemy")) {
			//Game Over Condition
			if (Player.Instance.Mass <= 1){
				Debug.Log ("game over");
				Destroy (this.gameObject);
				Player.Instance.Mass--;
			} 
			// Mass Loss Condition
			else if (Player.Instance.Mass < 5) {
				Debug.Log ("Damage Taken\nCurrent Mass: " + Player.Instance.Mass + "\n");
				shrink (this.gameObject);
				shrink (GameObject.FindGameObjectWithTag ("masspickup"));
				other.gameObject.GetComponent<MissleController> ().Reset ();
				Player.Instance.Mass--;			
			}
			// Asteroid too powerful for rockets
			else {
				Destroy (other.gameObject);
			}
		}
	}

	public void grow(GameObject obj){
		// Add current scale to scale (doubling size)
		Transform t = obj.GetComponent<Transform> ();
		t.localScale += t.localScale;

	}	

	public void shrink(GameObject obj){
		// Decrease Scale by half
		Transform t = obj.GetComponent<Transform> ();
		// current scale
		float cs = t.localScale.x;
		t.localScale -= new Vector3(cs*0.5f, cs*0.5f, cs*0.5f);
	}


}
