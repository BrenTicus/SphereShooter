using UnityEngine;
using System.Collections;

public class ParticleDeath : MonoBehaviour {
	public float lifeTime;

	float timeAlive;

	// Use this for initialization
	void Start () {
		timeAlive = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timeAlive > lifeTime) Destroy(gameObject);
	}
}
