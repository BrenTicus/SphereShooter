using UnityEngine;
using System.Collections;

public class KeepScore : MonoBehaviour {
	public int score;
	UnityEngine.UI.Text text;

	// Use this for initialization
	void Start () {
		score = 0;
		text = GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void UpdateScore () {
		score++;
		text.text = score.ToString();
	}
}
