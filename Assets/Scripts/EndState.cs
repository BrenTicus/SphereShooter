using UnityEngine;
using System.Collections;

public class EndState : MonoBehaviour {
	public GameObject loseText;
	public GameObject goal;
	
	void Update () {
		if(goal == null)
		{
			loseText.SetActive(true);
			if (Input.GetAxis("Enter") > 0)
			{
				Application.LoadLevel(1);
			}
		}
	}
}
