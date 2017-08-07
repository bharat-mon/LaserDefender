using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	Text text;
	public static int score = 0;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	public void Score (int points) {
		score += points;
		text.text = "Score  " + score.ToString();
	}
	
	public static void Reset () {
		score = 0;
	}
}
