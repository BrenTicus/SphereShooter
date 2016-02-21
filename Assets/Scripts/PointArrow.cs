using UnityEngine;
using System.Collections;

public class PointArrow : MonoBehaviour {
	public GameObject enemies;
	public GameObject player;
	public Vector3 average;
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + new Vector3(0, 1.5f, 0);
		average = new Vector3(0,0,0);
		foreach (Transform child in enemies.transform)
		{
			average += child.position;
		}
		average /= enemies.transform.childCount;

		transform.LookAt(average);
	}
}
