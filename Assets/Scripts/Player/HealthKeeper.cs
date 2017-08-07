using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthKeeper : MonoBehaviour {
	Text text;
	
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	public void Health (float health) {
		text.text = "HP  " + health.ToString();
	}
}
