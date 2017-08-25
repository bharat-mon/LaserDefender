using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthKeeper : MonoBehaviour {
	Text healthText;
	
	// Use this for initialization
	void Start () {
		healthText = GetComponent<Text>();
	}
	
	public void Health (float health) {
		healthText.text = "HP  " + health.ToString();
	}
}
