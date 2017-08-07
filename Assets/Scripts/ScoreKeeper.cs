using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	Text text;
	private int score = 0;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		Reset();
	}
	
	public void Score (int points) {
		score += points;
		text.text = "Score  " + score.ToString();
	}
	
	public void Reset () {
		score = 0;
		text.text = "Score  " + score.ToString();
	}
}
