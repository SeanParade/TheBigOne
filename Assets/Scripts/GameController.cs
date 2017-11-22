using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Used for enemy logic
	private GameObject[] enemies;

	private GameObject lastPlayer;

	[SerializeField]
	GameObject missle;
	[SerializeField]
	Text labelmass;
	[SerializeField]
	Text labelscore;	
	[SerializeField]
	Text label_win_lose;
	[SerializeField]
	Button playagain;
	[SerializeField]
	GameObject player;

	public void Initialize(){
		// destroy remaining player and enemy objects
		if (GameObject.FindGameObjectWithTag ("player") != null) {
			Destroy (GameObject.FindGameObjectWithTag ("player"));
		}
		enemies = GameObject.FindGameObjectsWithTag("enemy");
		foreach (var enemy in enemies) {
			Destroy (enemy);
		}

		// init score/mass
		Player.Instance.Score = 0;
		Player.Instance.Mass = 1;
		Instantiate (missle);
		Instantiate (player);

		labelmass.gameObject.SetActive (true);
		labelscore.gameObject.SetActive (true);
		label_win_lose.gameObject.SetActive (false);
		playagain.gameObject.SetActive (false);

		StartCoroutine ("AddMissle");
	}

	public void updateUI(){
		labelmass.text = "Mass: " + Player.Instance.Mass;
		labelscore.text = "Score: " + Player.Instance.Score;
	}

	public void gameOver(){
		labelmass.gameObject.SetActive (false);
		labelscore.gameObject.SetActive (false);
		label_win_lose.text = "YOU LOSE!";
		label_win_lose.gameObject.SetActive (true);
		playagain.gameObject.SetActive (true);

	}

	public void winner(){
		labelmass.gameObject.SetActive (false);
		labelscore.gameObject.SetActive (false);
		label_win_lose.text = "YOU WIN!";
		label_win_lose.gameObject.SetActive (true);
		playagain.gameObject.SetActive (true);

	}

	public void Start(){
		Player.Instance.gctl = this;
		Initialize ();
	}


	private IEnumerator AddMissle(){
		enemies = GameObject.FindGameObjectsWithTag("enemy");
		int enemyAmount = enemies.Length;
		int time = Random.Range (1, 20);
		yield return new WaitForSeconds ((float)time);
		Instantiate (missle);

		Debug.Log ("Enemies in play: " + enemyAmount);

		// Limit active enemies to 5
		if (enemyAmount > 3) {
			yield break;
		}
			StartCoroutine ("AddMissle");
		
	}

}
